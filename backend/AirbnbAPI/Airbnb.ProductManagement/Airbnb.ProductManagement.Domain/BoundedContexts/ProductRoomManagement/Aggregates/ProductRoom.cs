using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRoomManagement.Aggregates;

public class ProductRoom : AggregateRoot
{
    public int ProductId { get; private set; }
    public int RoomId { get; private set; }

    public ProductRoom() { }

    public ProductRoom(int productId, int roomId)
    {
        ProductId = productId;
        RoomId = roomId;

        RaiseEvent(new ProductRoomCreatedEvent(Id, productId, roomId));
    }

    public void Update(int productId, int roomId)
    {
        ProductId = productId;
        RoomId = roomId;

        RaiseEvent(new ProductRoomUpdatedEvent(Id, productId, roomId));
    }

    public void Delete()
    {
        RaiseEvent(new ProductRoomDeletedEvent(Id));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case ProductRoomCreatedEvent e:
                OnProductRoomCreatedEvent(e);
                break;
            case ProductRoomUpdatedEvent e:
                OnProductRoomUpdatedEvent(e);
                break;
            case ProductRoomDeletedEvent e:
                OnProductRoomDeletedEvent(e);
                break;
        }
    }

    private void OnProductRoomCreatedEvent(ProductRoomCreatedEvent @event)
    {
        Id = @event.AggregateId;
        ProductId = @event.ProductId;
        RoomId = @event.RoomId;
    }

    private void OnProductRoomUpdatedEvent(ProductRoomUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        ProductId = @event.ProductId;
        RoomId = @event.RoomId;
    }

    private void OnProductRoomDeletedEvent(ProductRoomDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion
}