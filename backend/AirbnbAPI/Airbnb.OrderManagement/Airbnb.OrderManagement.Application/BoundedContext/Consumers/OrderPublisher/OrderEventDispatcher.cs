using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductOrder;
using Airbnb.ProductManagement.Application.BoundedContext.ProductReviewUpdatedConsumer.OrderConsumer;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductReviewUpdatedConsumer;

public class OrderEventDispatcher : IOrderEventDispatcher
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderEventDispatcher> _logger;
    private readonly IBus _bus;
    
    public OrderEventDispatcher(
        IMediator mediator,
        ILogger<OrderEventDispatcher> logger, IBus bus)
    {
        _mediator = mediator;
        _logger = logger;
        _bus = bus;
    }

    public async Task DispatchAsync(IOrderEvent evt, CancellationToken ct)
    {
        _logger.LogInformation("Handling {EventType} for ProductId={ProductId}", evt.GetType().Name, evt.ProductId);

        switch (evt)
        {
            case ProductOrderCreatedEvent created:
                _logger.LogInformation("→ Order Created: {OrderId} from {DateStart} to {DateEnd}", created.OrderId,
                    created.DateStart, created.DateEnd);

                await _bus.Publish(created, ct);
                break;

            case ProductOrderUpdatedEvent updated:
                _logger.LogInformation("→ Order Updated: {OrderId} from {DateStart} to {DateEnd}", updated.OrderId, updated.DateStart, updated.DateEnd);
                
                await _bus.Publish(updated, ct);
                break;

            case ProductOrderDeletedEvent deleted:
                _logger.LogInformation("→ Order Deleted: {OrderId}", deleted.OrderId);
                
                await _bus.Publish(deleted, ct);
                break;
        }

        /*
        await _mediator.Publish(new ProductUpdatedEvent(
            product.Id, product.Title, product.Description,
            product.Price, product.IsAvailable, product.CreatedAt,
            product.UserId, product.ApartmentTypeId, product.AddressLegalId
        ), ct);
        */
    }
}