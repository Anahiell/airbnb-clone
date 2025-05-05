using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.FileService;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands;

public class UploadProductImageCommandHandler : ICommandHandler<UploadProductPictureCommand, Result<List<int>>>
{
    private readonly IFileService _fileService;
    private readonly IRepository<ProductPicture> _productPictureRepository;
    private readonly IMediator _mediator;

    public UploadProductImageCommandHandler(IWebHostEnvironment env, IRepository<ProductPicture> productPictureRepository, IMediator mediator, IFileService fileService)
    {
        _productPictureRepository = productPictureRepository;
        _mediator = mediator;
        _fileService = fileService;
    }

    public async Task<Result<List<int>>> Handle(UploadProductPictureCommand request, CancellationToken cancellationToken)
    {
        if (request.Files.Count == 0 || request.ProductId <= 0)
        {
            // return Result<List<string>>.Failure("Некорректный запрос");
        }

        var createdIds = new List<int>();

        foreach (var file in request.Files)
        {
            var relativeUrl = await _fileService.SaveAsync(file, "Product", cancellationToken);

            var picture = new ProductPicture(Guid.NewGuid(), relativeUrl, request.ProductId, DateTime.UtcNow);
            var id = await _productPictureRepository.AddAsync(picture, cancellationToken);

            await _mediator.Publish(new ProductPictureCreatedEvent(picture.Id, picture.PictureGuid, relativeUrl, request.ProductId, picture.CreatedAt), cancellationToken);
            createdIds.Add(id);
        }

        return Result<List<int>>.Success(createdIds);
    }
}