using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;

public class ProductRoomCreatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public int ProductId { get; }
    public int RoomId { get; }

    public ProductRoomCreatedEvent(int aggregateId, int productId, int roomId)
    {
        AggregateId = aggregateId;
        ProductId = productId;
        RoomId = roomId;
    }
}