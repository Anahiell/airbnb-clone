using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Events;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Aggregates;


public class ProductFacility : AggregateRoot
{
    public int ProductId { get; private set; }
    public int FacilityId { get; private set; }

    public ProductFacility() { }

    public ProductFacility(int productId, int facilityId)
    {
        ProductId = productId;
        FacilityId = facilityId;
        RaiseEvent(new ProductFacilityCreatedEvent(Id, productId, facilityId));
    }

    public void Update(int productId, int facilityId)
    {
        ProductId = productId;
        FacilityId = facilityId;
        RaiseEvent(new ProductFacilityUpdatedEvent(Id, productId, facilityId));
    }

    public void Delete()
    {
        RaiseEvent(new ProductFacilityDeletedEvent(Id));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case ProductFacilityCreatedEvent e:
                OnProductFacilityCreatedEvent(e);
                break;
            case ProductFacilityUpdatedEvent e:
                OnProductFacilityUpdatedEvent(e);
                break;
            case ProductFacilityDeletedEvent e:
                OnProductFacilityDeletedEvent(e);
                break;
        }
    }

    private void OnProductFacilityCreatedEvent(ProductFacilityCreatedEvent @event)
    {
        Id = @event.AggregateId;
        ProductId = @event.ProductId;
        FacilityId = @event.FacilityId;
    }

    private void OnProductFacilityUpdatedEvent(ProductFacilityUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        ProductId = @event.ProductId;
        FacilityId = @event.FacilityId;
    }

    private void OnProductFacilityDeletedEvent(ProductFacilityDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion
}