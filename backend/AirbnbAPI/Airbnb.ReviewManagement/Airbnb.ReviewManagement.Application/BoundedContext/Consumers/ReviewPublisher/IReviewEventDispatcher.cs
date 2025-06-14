using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductReviewUpdatedConsumer.ReviewConsumer;

public interface IReviewEventDispatcher
{
    Task DispatchAsync(IReviewEvent evt, CancellationToken cancellationToken);
}