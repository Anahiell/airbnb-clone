using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductTag;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.TagConsumer;

public class ProductTagUpdatedHandlerStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductTagUpdatedHandlerStrategy> _logger;

    public ProductTagUpdatedHandlerStrategy(IMediator mediator, ILogger<ProductTagUpdatedHandlerStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductTagUpdatedEvent;

    public async Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var updated = (ProductTagUpdatedEvent)@event;
        _logger.LogInformation("Handling ProductTagUpdatedEvent: ProductId={ProductId}, TagId={TagId}", updated.ProductId, updated.TagId);
        await _mediator.Publish(updated, cancellationToken);
    }
}