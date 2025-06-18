using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;

public class ProductRoomUpdatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public int ProductId { get; }
    public int RoomId { get; }
    public string? RoomName { get; }

    public ProductRoomUpdatedEvent(int aggregateId, int productId, int roomId, string? roomName = null)
    {
        AggregateId = aggregateId;
        ProductId = productId;
        RoomId = roomId;
        RoomName = roomName;
    }
}