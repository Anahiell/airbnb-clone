using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Events;

public class FeatureDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }

    public FeatureDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}