using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductCoordinateManagement.Events;

public class CoordinateDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }

    public CoordinateDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}