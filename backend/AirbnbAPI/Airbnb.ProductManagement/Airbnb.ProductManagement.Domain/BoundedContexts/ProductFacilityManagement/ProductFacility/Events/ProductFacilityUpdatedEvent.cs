using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Events;

public class ProductFacilityUpdatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public int ProductId { get; }
    public int FacilityId { get; }

    public ProductFacilityUpdatedEvent(int aggregateId, int productId, int facilityId)
    {
        AggregateId = aggregateId;
        ProductId = productId;
        FacilityId = facilityId;
    }
}