using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Events;

public class ProductFeatureUpdatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public int ProductId { get; }
    public int FeatureId { get; }

    public ProductFeatureUpdatedEvent(int aggregateId, int productId, int featureId)
    {
        AggregateId = aggregateId;
        ProductId = productId;
        FeatureId = featureId;
    }
}