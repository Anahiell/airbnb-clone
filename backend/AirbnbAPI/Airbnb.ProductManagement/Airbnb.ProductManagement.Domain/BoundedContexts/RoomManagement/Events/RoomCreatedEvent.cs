using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;

public class RoomCreatedEvent : DomainEvent
{
    public int AggregateId { get; }
    public string Name { get; }
    public int Capacity { get; }

    public RoomCreatedEvent(int aggregateId, string name, int capacity)
    {
        AggregateId = aggregateId;
        Name = name;
        Capacity = capacity;
    }
}