using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductOrder;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.OrderConsumer;

public class ProductOrderUpdatedHandlerStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductOrderUpdatedHandlerStrategy> _logger;

    public ProductOrderUpdatedHandlerStrategy(IMediator mediator, ILogger<ProductOrderUpdatedHandlerStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductOrderUpdatedEvent;

    public async Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var updated = (ProductOrderUpdatedEvent)@event;

        _logger.LogInformation("Handling ProductOrderUpdatedEvent for OrderId={OrderId}", updated.OrderId);

        /*
        await _mediator.Publish(new ProductUpdatedEvent(
            updated.ProductId,
        ), cancellationToken);
        */
    }
}