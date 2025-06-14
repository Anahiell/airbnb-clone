using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Events;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Aggregates;

public class Facility : AggregateRoot
{
    public string IconName { get; private set; }
    public string Name { get; private set; }

    public Facility() { }

    public Facility(string iconName, string name)
    {
        IconName = iconName;
        Name = name;
        RaiseEvent(new FacilityCreatedEvent(Id, iconName, name));
    }

    public void Update(string iconName, string name)
    {
        IconName = iconName;
        Name = name;
        RaiseEvent(new FacilityUpdatedEvent(Id, iconName, name));
    }

    public void Delete()
    {
        RaiseEvent(new FacilityDeletedEvent(Id));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case FacilityCreatedEvent e:
                OnFacilityCreatedEvent(e);
                break;
            case FacilityUpdatedEvent e:
                OnFacilityUpdatedEvent(e);
                break;
            case FacilityDeletedEvent e:
                OnFacilityDeletedEvent(e);
                break;
        }
    }

    private void OnFacilityCreatedEvent(FacilityCreatedEvent @event)
    {
        Id = @event.AggregateId;
        IconName = @event.IconName;
        Name = @event.Name;
    }

    private void OnFacilityUpdatedEvent(FacilityUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        IconName = @event.IconName;
        Name = @event.Name;
    }

    private void OnFacilityDeletedEvent(FacilityDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion
}