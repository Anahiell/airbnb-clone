using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductManagement.Events;

public class ProductArchivedEvent : IDomainEvent
{
    public int AggregateId { get; private set; }
    public bool IsAvailable { get; private set; }

    public ProductArchivedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
        IsAvailable = false;
    }
}
