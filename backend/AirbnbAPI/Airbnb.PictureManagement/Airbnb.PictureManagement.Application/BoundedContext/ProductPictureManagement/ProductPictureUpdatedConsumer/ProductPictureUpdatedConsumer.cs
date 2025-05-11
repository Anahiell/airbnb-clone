using Airbnb.PictureManagement.Application.BoundedContext.FileService;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProductPictureUpdatedEvent = Airbnb.ProductManagement.Application.BoundedContext.Events.ProductPictureUpdatedEvent;

namespace Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.ProductPictureUpdatedConsumer;

public class ProductPictureUpdatedConsumer : IConsumer<ProductPictureUpdatedEvent>
{
    private readonly IRepository<ProductPicture> _productPictureRepository;
    private readonly ILogger<ProductPictureUpdatedConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IFileService _fileService;
    public ProductPictureUpdatedConsumer(
        IRepository<ProductPicture> productPictureRepository,
        ILogger<ProductPictureUpdatedConsumer> logger,
        IFileService fileService,
        IMediator mediator)
    {
        _productPictureRepository = productPictureRepository;
        _logger = logger;
        _mediator = mediator;
        _fileService = fileService;
    }

    public async Task Consume(ConsumeContext<ProductPictureUpdatedEvent> context)
    {
        var eventMessage = context.Message;
        _logger.LogInformation("Start processing ProductPictureUpdatedEvent for ProductId: {ProductId}", eventMessage.ProductId);

        try
        {
            var stream = new MemoryStream(eventMessage.PictureData);
            var formFile = new FormFile(stream, 0, eventMessage.PictureData.Length, "picture", $"{Guid.NewGuid()}.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var relativeUrl = await _fileService.SaveAsync(formFile, "Product", context.CancellationToken);

            var picture = new ProductPicture(Guid.NewGuid(), relativeUrl, eventMessage.ProductId, DateTime.UtcNow);
            await _productPictureRepository.AddAsync(picture, context.CancellationToken);

            await _mediator.Publish(new ProductPictureCreatedEvent(
                picture.Id, picture.PictureGuid, relativeUrl, picture.ProductId, picture.CreatedAt), context.CancellationToken);

            _logger.LogInformation("Successfully processed and saved picture for ProductId: {ProductId}", eventMessage.ProductId);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process ProductPictureUpdatedEvent for ProductId: {ProductId}", eventMessage.ProductId);
            throw;
        }

        _logger.LogInformation("Finished processing ProductPictureUpdatedEvent.");
    }
}