using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;

public class ProductRoomDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }

    public ProductRoomDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}