using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductOrder;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.OrderConsumer;

public class ProductOrderDeletedHandlerStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductOrderDeletedHandlerStrategy> _logger;

    public ProductOrderDeletedHandlerStrategy(IMediator mediator, ILogger<ProductOrderDeletedHandlerStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductOrderDeletedEvent;

    public async Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var deleted = (ProductOrderDeletedEvent)@event;

        _logger.LogInformation("Handling ProductOrderDeletedEvent for OrderId={OrderId}", deleted.OrderId);

        await _mediator.Publish(deleted, cancellationToken);
    }
}
