using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Events;

public class ProductAdvantageCreatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public int ProductId { get; }
    public string Title { get; }
    public string Description { get; }

    public ProductAdvantageCreatedEvent(int aggregateId, int productId, string title, string description)
    {
        AggregateId = aggregateId;
        ProductId = productId;
        Title = title;
        Description = description;
    }
}