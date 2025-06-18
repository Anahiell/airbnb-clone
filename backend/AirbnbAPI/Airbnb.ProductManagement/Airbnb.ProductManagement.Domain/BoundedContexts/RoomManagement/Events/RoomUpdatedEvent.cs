using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;

public class RoomUpdatedEvent : DomainEvent
{
    public int AggregateId { get; }
    public string Name { get; }
    public int Capacity { get; }

    public RoomUpdatedEvent(int aggregateId, string name, int capacity)
    {
        AggregateId = aggregateId;
        Name = name;
        Capacity = capacity;
    }
}