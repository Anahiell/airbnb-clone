using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Events;

public class ProductFacilityDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }

    public ProductFacilityDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}