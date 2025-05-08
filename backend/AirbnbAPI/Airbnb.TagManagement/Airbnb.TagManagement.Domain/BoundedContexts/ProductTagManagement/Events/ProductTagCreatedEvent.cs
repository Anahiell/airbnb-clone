using Airbnb.SharedKernel;

namespace Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Events;

public class ProductTagCreatedEvent : DomainEvent
{
    public int ProductId { get; }
    public int TagId { get; }

    public ProductTagCreatedEvent(int aggregateId, int productId, int tagId)
        : base(aggregateId)
    {
        ProductId = productId;
        TagId = tagId;
    }
}