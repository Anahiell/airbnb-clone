using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductReviewUpdatedConsumer.OrderConsumer;

public interface IOrderEventDispatcher
{
    Task DispatchAsync(IOrderEvent evt, CancellationToken cancellationToken);
}