using Airbnb.SharedKernel;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.ValueObjects;

public sealed class PassportNumber : ValueObject
{
    public string Value { get; }

    public PassportNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Passport number cannot be empty.", nameof(value));
        if (value.Length < 6 || value.Length > 12)
            throw new ArgumentException("Passport number length invalid.", nameof(value));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}