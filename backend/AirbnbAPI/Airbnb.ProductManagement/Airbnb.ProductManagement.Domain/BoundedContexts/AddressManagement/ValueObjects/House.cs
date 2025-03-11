using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address.AddressEnteties;

public class House : ValueObject
{
    public string Value { get; }

    public House()
    {
        
    }

    public House(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("House cannot be empty.");
        if (!int.TryParse(value, out _))
            throw new ArgumentException("House must be a number.");
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}