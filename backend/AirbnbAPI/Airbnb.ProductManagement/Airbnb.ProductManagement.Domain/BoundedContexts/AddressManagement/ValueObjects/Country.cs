using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address.AddressEnteties;

public class Country : ValueObject
{
    public string Value { get; }

    public Country()
    {
    }

    public Country(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Country cannot be empty.");
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}