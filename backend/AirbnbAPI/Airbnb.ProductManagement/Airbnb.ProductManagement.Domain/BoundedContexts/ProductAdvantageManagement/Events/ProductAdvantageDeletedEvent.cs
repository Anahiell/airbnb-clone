using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Events;

public class ProductAdvantageDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }

    public ProductAdvantageDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}