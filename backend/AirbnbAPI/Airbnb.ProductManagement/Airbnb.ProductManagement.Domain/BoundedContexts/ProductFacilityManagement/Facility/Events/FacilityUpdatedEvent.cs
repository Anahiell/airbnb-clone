using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Events;

public class FacilityUpdatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public string IconName { get; }
    public string Name { get; }

    public FacilityUpdatedEvent(int aggregateId, string iconName, string name)
    {
        AggregateId = aggregateId;
        IconName = iconName;
        Name = name;
    }
}