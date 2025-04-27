using Airbnb.SharedKernel;

namespace Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Events;

public class OrderCreatedEvent : DomainEvent
{
    public int ProductId { get; private set; }
    public int UserId { get; private set; }
    public DateTime DateStart { get; private set; }
    public DateTime DateEnd { get; private set; }

    public OrderCreatedEvent(int aggregateId, int productId, int userId, DateTime dateStart, DateTime dateEnd)
        : base(aggregateId)
    {
        ProductId = productId;
        UserId = userId;
        DateStart = dateStart;
        DateEnd = dateEnd;
    }
}