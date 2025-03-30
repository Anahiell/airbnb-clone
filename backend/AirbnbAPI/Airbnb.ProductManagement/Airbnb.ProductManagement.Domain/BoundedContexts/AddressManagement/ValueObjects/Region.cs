using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address.AddressEnteties;

public class Region : ValueObject
{
    public string Value { get; }

    public Region()
    {
    }

    public Region(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Region cannot be empty.");
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}