using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.FileService;
using Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.ProductPictureUpdatedConsumer.ProductPicturePublisher;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using ProductPictureUpdatedEvent = Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture.ProductPictureUpdatedEvent;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands;

public class UploadProductImageCommandHandler : ICommandHandler<UploadProductPictureCommand, Result<List<int>>>
{
    private readonly IFileService _fileService;
    private readonly IRepository<ProductPicture> _productPictureRepository;
    private readonly IMediator _mediator;
    private readonly IProductPictureEventDispatcher _productPictureEventDispatcher;
    public UploadProductImageCommandHandler(IWebHostEnvironment env, IRepository<ProductPicture> productPictureRepository, IMediator mediator, IFileService fileService, IProductPictureEventDispatcher productPictureEventDispatcher)
    {
        _productPictureRepository = productPictureRepository;
        _mediator = mediator;
        _fileService = fileService;
        _productPictureEventDispatcher = productPictureEventDispatcher;
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

            await _productPictureEventDispatcher.DispatchAsync(
                new ProductPictureUpdatedEvent(picture.Id, picture.ProductId, picture.Url, picture.IsArchived,
                    picture.CreatedAt), cancellationToken);
        }

        return Result<List<int>>.Success(createdIds);
    }
}