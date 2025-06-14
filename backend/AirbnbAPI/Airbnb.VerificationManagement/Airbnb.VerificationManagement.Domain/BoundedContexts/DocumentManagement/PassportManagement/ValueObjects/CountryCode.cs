using Airbnb.SharedKernel;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.ValueObjects;

public sealed class CountryCode : ValueObject
{
    public string Value { get; }

    public CountryCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 2)
            throw new ArgumentException("Country code must be 2 letters ISO code.", nameof(value));
        Value = value.ToUpperInvariant();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}