using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Events;

public class ProductFacilitiesUpdatedEvent : DomainEvent
{
    public int ProductId { get; }
    public List<(int Id, string Name, string IconName)> Facilities { get; }

    public ProductFacilitiesUpdatedEvent(int productId, List<(int Id, string Name, string IconName)> facilities)
    {
        ProductId = productId;
        Facilities = facilities;
    }
}