using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductTag;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.ProductTagUpdatedConsumer.Publisher;

public class TagEventDispatcher : ITagEventDispatcher
{
    private readonly ILogger<TagEventDispatcher> _logger;
    private readonly IBus _bus;

    public TagEventDispatcher(ILogger<TagEventDispatcher> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task DispatchAsync(INotification evt, CancellationToken ct)
    {
        _logger.LogInformation("Handling {EventType}", evt.GetType().Name);

        switch (evt)
        {
            case ProductTagCreatedEvent created:
                _logger.LogInformation("→ Tag Added: TagId={TagId}, ProductId={ProductId}", created.TagId, created.ProductId);
                await _bus.Publish(created, ct);
                break;
            
            case ProductTagUpdatedEvent updated:
                _logger.LogInformation("→ Tag Updated: TagId={TagId}, ProductId={ProductId}, Name={TagName}", updated.TagId, updated.ProductId, updated.TagName);
                await _bus.Publish(updated, ct);
                break;

            case ProductTagDeletedEvent deleted:
                _logger.LogInformation("→ Tag Removed: TagId={TagId}, ProductId={ProductId}", deleted.TagId, deleted.ProductId);
                await _bus.Publish(deleted, ct);
                break;

            default:
                _logger.LogWarning("Unknown event type: {EventType}", evt.GetType().Name);
                break;
        }
    }
}