using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.ProductPictureUpdatedConsumer.ProductPicturePublisher;

public class ProductPictureEventDispatcher : IProductPictureEventDispatcher
{
    private readonly ILogger<ProductPictureEventDispatcher> _logger;
    private readonly IBus _bus;

    public ProductPictureEventDispatcher(ILogger<ProductPictureEventDispatcher> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task DispatchAsync(INotification evt, CancellationToken ct)
    {
        _logger.LogInformation("Handling {EventType}", evt.GetType().Name);

        switch (evt)
        {
            case ProductPictureCreatedEvent created:
                _logger.LogInformation("→ Picture Created: PictureId={PictureId}, ProductId={ProductId}", created.PictureId, created.ProductId);
                await _bus.Publish(created, ct);
                break;

            case ProductPictureUpdatedEvent updated:
                _logger.LogInformation("→ Picture Updated: PictureId={PictureId}, ProductId={ProductId}", updated.PictureId, updated.ProductId);
                await _bus.Publish(updated, ct);
                break;

            case ProductPictureDeletedEvent deleted:
                _logger.LogInformation("→ Picture Deleted: PictureId={PictureId}, ProductId={ProductId}", deleted.PictureId, deleted.ProductId);
                await _bus.Publish(deleted, ct);
                break;

            default:
                _logger.LogWarning("Unknown event type: {EventType}", evt.GetType().Name);
                break;
        }
    }
}