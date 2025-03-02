using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.ValueObjects;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;

public class ApartmentType : AggregateRoot
{
    public PropertyTypeEnum Value { get; private set; }

    public ApartmentType()
    {

    }

    public ApartmentType(PropertyTypeEnum value)
    {
        Value = value;
    }
}