using Airbnb.SharedKernel;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.ValueObjects;

public sealed class Authority : ValueObject
{
    public string Value { get; }

    public Authority(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Authority cannot be empty.", nameof(value));
        if (value.Length > 100)
            throw new ArgumentException("Authority is too long.", nameof(value));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}