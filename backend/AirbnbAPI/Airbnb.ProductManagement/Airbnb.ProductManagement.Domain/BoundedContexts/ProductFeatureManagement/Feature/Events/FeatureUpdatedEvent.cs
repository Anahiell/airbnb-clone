using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Events;

public class FeatureUpdatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public string Name { get; }
    public bool Forcibly { get; }
    public int Price { get; }

    public FeatureUpdatedEvent(int aggregateId, string name, bool forcibly, int price)
    {
        AggregateId = aggregateId;
        Name = name;
        Forcibly = forcibly;
        Price = price;
    }
}