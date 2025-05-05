using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.FileService;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands.UpdatePictureCommand;

public class UpdateProductImageCommandHandler : ICommandHandler<UpdateProductImageCommand, Result>
{
    private readonly IRepository<ProductPicture> _productPictureRepository;
    private readonly IMediator _mediator;
    private readonly IFileService _fileService;

    public UpdateProductImageCommandHandler(IRepository<ProductPicture> productPictureRepository, IMediator mediator,
        IFileService fileService)
    {
        _productPictureRepository = productPictureRepository;
        _mediator = mediator;
        _fileService = fileService;
    }

    public async Task<Result> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
    {
        var picture = await _productPictureRepository.GetByIdAsync(request.Id, cancellationToken);
        if (picture is null)
        {
            // return Result.Failure("Картинка не найдена");   
        }

        await _fileService.DeleteAsync(picture.Url,"Product");
        
        var newUrl  = await _fileService.SaveAsync(request.File, "Product", cancellationToken);

        picture.UpdatePicture(newUrl);

        await _productPictureRepository.UpdateAsync(picture, cancellationToken);

        await _mediator.Publish(new ProductPictureUpdatedEvent(picture.Id, picture.ProductId, picture.PictureGuid, picture.Url),
            cancellationToken);

        return Result.Success();
    }
}