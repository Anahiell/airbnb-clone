using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Events;

public class FacilityDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }

    public FacilityDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}