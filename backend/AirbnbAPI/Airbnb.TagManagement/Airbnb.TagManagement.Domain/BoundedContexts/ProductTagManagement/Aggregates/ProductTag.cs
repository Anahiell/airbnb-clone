using Airbnb.SharedKernel;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Events;

namespace Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Aggregates;

public class ProductTag : AggregateRoot
{
    public int ProductId { get; private set; }
    public int TagId { get; private set; }

    public ProductTag()
    {
    }

    public ProductTag(int productId, int tagId)
    {
        ProductId = productId;
        TagId = tagId;

        RaiseEvent(new ProductTagCreatedEvent(Id, productId, tagId));
    }

    #region Aggregate Methods

    public void UpdateProductTag(int newProductId, int newTagId)
    {
        ProductId = newProductId;
        TagId = newTagId;

        RaiseEvent(new ProductTagUpdatedEvent(Id, newProductId, newTagId));
    }

    public void Delete()
    {
        RaiseEvent(new ProductTagDeletedEvent(Id));
    }

    #endregion

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case ProductTagCreatedEvent e:
                OnProductTagCreated(e);
                break;
            case ProductTagUpdatedEvent e:
                OnProductTagUpdated(e);
                break;
            case ProductTagDeletedEvent e:
                OnProductTagDeleted(e);
                break;
        }
    }

    private void OnProductTagCreated(ProductTagCreatedEvent e)
    {
        Id = e.AggregateId;
        ProductId = e.ProductId;
        TagId = e.TagId;
    }

    private void OnProductTagUpdated(ProductTagUpdatedEvent e)
    {
        ProductId = e.NewProductId;
        TagId = e.NewTagId;
    }

    private void OnProductTagDeleted(ProductTagDeletedEvent e)
    {
        Id = e.AggregateId;
    }

    #endregion
}