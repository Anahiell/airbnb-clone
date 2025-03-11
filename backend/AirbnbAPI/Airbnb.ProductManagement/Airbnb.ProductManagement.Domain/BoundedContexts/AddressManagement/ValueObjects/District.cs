using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address.AddressEnteties;

public class District : ValueObject
{
    public string Value { get; }

    public District()
    {
        
    }

    public District(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("District cannot be empty.");
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
