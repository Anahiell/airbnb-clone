using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Events;

public class ProductFeaturesUpdatedEvent : DomainEvent
{
    public int ProductId { get; }
    public List<(int Id, string Name, bool Forcibility, int Price)> FeatureNames { get; }

    public ProductFeaturesUpdatedEvent(int productId, List<(int Id, string Name, bool Forcibility, int Price)> featureNames)
    {
        ProductId = productId;
        FeatureNames = featureNames;
    }
}