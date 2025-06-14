using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Events;

public class ProductFeatureDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }

    public ProductFeatureDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}