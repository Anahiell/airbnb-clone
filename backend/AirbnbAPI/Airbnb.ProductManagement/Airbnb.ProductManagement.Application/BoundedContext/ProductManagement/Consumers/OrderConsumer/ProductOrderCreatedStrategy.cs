using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductOrder;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.OrderConsumer;

public class ProductOrderCreatedStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductOrderCreatedStrategy> _logger;

    public ProductOrderCreatedStrategy(IMediator mediator, ILogger<ProductOrderCreatedStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductOrderUpdatedEvent;

    public Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var evt = (ProductOrderUpdatedEvent)@event;
        _logger.LogInformation("→ Order Created Strategy: {OrderId}", evt.OrderId);
        return _mediator.Publish(evt, cancellationToken);
    }
}
