using Airbnb.SharedKernel;

namespace Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Events;

public class ProductTagDeletedEvent : DomainEvent
{
    public ProductTagDeletedEvent(int aggregateId)
        : base(aggregateId)
    {
    }
}