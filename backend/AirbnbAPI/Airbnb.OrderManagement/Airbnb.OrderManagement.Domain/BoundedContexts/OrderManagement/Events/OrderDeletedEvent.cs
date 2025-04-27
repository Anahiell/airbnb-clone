using Airbnb.SharedKernel;

namespace Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Events;

public class OrderDeletedEvent : DomainEvent
{
    public int Id { get; private set; }

    public OrderDeletedEvent(int aggregateId)
        : base(aggregateId)
    {
        Id = aggregateId;
    }
}