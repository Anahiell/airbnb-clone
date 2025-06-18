using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.RoomManagement.Aggregates;

public class Room : AggregateRoot
{
    public string Name { get; private set; }
    public int Capacity { get; private set; }

    public Room() { }

    public Room(string name, int capacity)
    {
        Name = name;
        Capacity = capacity;

        RaiseEvent(new RoomCreatedEvent(Id, name, capacity));
    }

    public void Update(string name, int capacity)
    {
        Name = name;
        Capacity = capacity;

        RaiseEvent(new RoomUpdatedEvent(Id, name, capacity));
    }

    public void Delete()
    {
        RaiseEvent(new RoomDeletedEvent(Id));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case RoomCreatedEvent e:
                OnRoomCreatedEvent(e);
                break;
            case RoomUpdatedEvent e:
                OnRoomUpdatedEvent(e);
                break;
            case RoomDeletedEvent e:
                OnRoomDeletedEvent(e);
                break;
        }
    }

    private void OnRoomCreatedEvent(RoomCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Name = @event.Name;
        Capacity = @event.Capacity;
    }

    private void OnRoomUpdatedEvent(RoomUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        Name = @event.Name;
        Capacity = @event.Capacity;
    }

    private void OnRoomDeletedEvent(RoomDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion
}