using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Events;

public class FacilityCreatedEvent : DomainEvent
{
    public int AggregateId { get; }
    public string IconName { get; }
    public string Name { get; }

    public FacilityCreatedEvent(int aggregateId, string iconName, string name)
    {
        AggregateId = aggregateId;
        IconName = iconName;
        Name = name;
    }
}