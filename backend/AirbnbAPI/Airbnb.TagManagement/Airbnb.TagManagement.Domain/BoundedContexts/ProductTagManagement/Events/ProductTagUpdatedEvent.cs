using Airbnb.SharedKernel;

namespace Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Events;

public class ProductTagUpdatedEvent : DomainEvent
{
    public int NewProductId { get; }
    public int NewTagId { get; }

    public ProductTagUpdatedEvent(int aggregateId, int newProductId, int newTagId)
        : base(aggregateId)
    {
        NewProductId = newProductId;
        NewTagId = newTagId;
    }
}