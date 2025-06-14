using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Events;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Aggregates;

public class ProductFeature : AggregateRoot
{
    public int ProductId { get; private set; }
    public int FeatureId { get; private set; }

    public ProductFeature() { }

    public ProductFeature(int productId, int featureId)
    {
        ProductId = productId;
        FeatureId = featureId;
        RaiseEvent(new ProductFeatureCreatedEvent(Id, productId, featureId));
    }

    public void Update(int productId, int featureId)
    {
        ProductId = productId;
        FeatureId = featureId;
        RaiseEvent(new ProductFeatureUpdatedEvent(Id, productId, featureId));
    }

    public void Delete()
    {
        RaiseEvent(new ProductFeatureDeletedEvent(Id));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case ProductFeatureCreatedEvent e:
                OnCreated(e);
                break;
            case ProductFeatureUpdatedEvent e:
                OnUpdated(e);
                break;
            case ProductFeatureDeletedEvent e:
                OnDeleted(e);
                break;
        }
    }

    private void OnCreated(ProductFeatureCreatedEvent e)
    {
        Id = e.AggregateId;
        ProductId = e.ProductId;
        FeatureId = e.FeatureId;
    }

    private void OnUpdated(ProductFeatureUpdatedEvent e)
    {
        Id = e.AggregateId;
        ProductId = e.ProductId;
        FeatureId = e.FeatureId;
    }

    private void OnDeleted(ProductFeatureDeletedEvent e)
    {
        Id = e.AggregateId;
    }

    #endregion
}