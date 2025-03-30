using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductManagement.Events;

public class ProductDeletedEvent : DomainEvent
{
    public int Id { get; private set; }

    public ProductDeletedEvent(int aggregateId)
        : base(aggregateId)
    {
        Id = aggregateId;
    }
}