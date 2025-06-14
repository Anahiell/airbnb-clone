using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.FileService;
using Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.ProductPictureUpdatedConsumer.ProductPicturePublisher;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands.DeletePictureCommand;

public class DeleteProductImageCommandHandler : ICommandHandler<DeleteProductImageCommand, Result>
{
    private readonly IRepository<ProductPicture> _productPictureRepository;
    private readonly IMediator _mediator;
    private readonly IFileService _fileService;
    private readonly IProductPictureEventDispatcher _productPictureEventDispatcher;
    public DeleteProductImageCommandHandler(IRepository<ProductPicture> productPictureRepository, IMediator mediator, IFileService fileService, IProductPictureEventDispatcher productPictureEventDispatcher)
    {
        _productPictureRepository = productPictureRepository;
        _mediator = mediator;
        _fileService = fileService;
        _productPictureEventDispatcher = productPictureEventDispatcher;
    }

    public async Task<Result> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var picture = await _productPictureRepository.GetByIdAsync(request.Id, cancellationToken);
        if (picture is null)
            // return Result.Failure("Картинка не найдена");

        await _productPictureRepository.DeleteAsync(request.Id, cancellationToken);
        
        await _fileService.DeleteAsync(picture.Url, "Product");

        await _mediator.Publish(new ProductPictureDeletedEvent(picture.Id, picture.PictureGuid), cancellationToken);
        
        await _productPictureEventDispatcher.DispatchAsync(
            new ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture.ProductPictureDeletedEvent
            {
                PictureId = picture.Id,
                ProductId = picture.ProductId
            }, cancellationToken);

        return Result.Success();
    }
}