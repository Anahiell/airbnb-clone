using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;

public class RoomDeletedEvent : DomainEvent
{
    public int AggregateId { get; }

    public RoomDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}