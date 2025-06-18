using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Events;

public class ProductAdvantageUpdatedEvent : DomainEvent
{
    public int AggregateId { get; }
    public string Title { get; }
    public string Description { get; }

    public ProductAdvantageUpdatedEvent(int aggregateId, string title, string description)
    {
        AggregateId = aggregateId;
        Title = title;
        Description = description;
    }
}