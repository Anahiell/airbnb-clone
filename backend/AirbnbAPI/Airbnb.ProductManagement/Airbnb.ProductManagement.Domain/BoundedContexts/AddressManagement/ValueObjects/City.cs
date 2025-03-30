using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address.AddressEnteties;

public class City : ValueObject
{
    public string Value { get; }

    public City()
    {
    }

    public City(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("City cannot be empty.");
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}