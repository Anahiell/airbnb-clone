using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address.AddressEnteties;

public class Flat : ValueObject
{
    public string Value { get; }

    public Flat()
    {
    }

    public Flat(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}