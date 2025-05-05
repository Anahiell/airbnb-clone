using Airbnb.Domain.BoundedContexts.ApartmentTypeManagement.Events;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.ValueObjects;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;

public class ApartmentType : AggregateRoot
{
    public PropertyTypeEnum Value { get; private set; }

    public ApartmentType()
    {
    }

    public static ApartmentType GetApartmentTypeByValue(PropertyTypeEnum value)
    {
        var apartmentTypes = new List<ApartmentType>
        {
            new ApartmentType(PropertyTypeEnum.Apartment),
            new ApartmentType(PropertyTypeEnum.House),
            new ApartmentType(PropertyTypeEnum.Townhouse)
        };

        return apartmentTypes.FirstOrDefault(at => at.Value == value);
    }
    
    public ApartmentType(PropertyTypeEnum value)
    {
        Value = value;
    }

    #region AggregateMethods

    public void CreateApartmentType(PropertyTypeEnum value)
    {
        Value = value;
        RaiseEvent(new ApartmentTypeCreatedEvent(Id, value));
    }

    public void UpdateApartmentType(PropertyTypeEnum value)
    {
        Value = value;
        RaiseEvent(new ApartmentTypeUpdatedEvent(Id, value));
    }

    public void DeleteApartmentType()
    {
        RaiseEvent(new ApartmentTypeDeletedEvent(Id));
    }

    #endregion

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case ApartmentTypeCreatedEvent e:
                OnApartmentTypeCreatedEvent(e);
                break;
            case ApartmentTypeUpdatedEvent e:
                OnApartmentTypeUpdatedEvent(e);
                break;
            case ApartmentTypeDeletedEvent e:
                OnApartmentTypeDeletedEvent(e);
                break;
        }
    }

    private void OnApartmentTypeCreatedEvent(ApartmentTypeCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Value = @event.Value;
    }

    private void OnApartmentTypeUpdatedEvent(ApartmentTypeUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        Value = @event.Value;
    }

    private void OnApartmentTypeDeletedEvent(ApartmentTypeDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }
    
    #endregion
}