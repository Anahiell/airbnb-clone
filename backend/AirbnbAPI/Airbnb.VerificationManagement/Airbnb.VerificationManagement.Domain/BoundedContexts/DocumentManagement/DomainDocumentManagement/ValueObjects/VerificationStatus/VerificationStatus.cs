using Airbnb.SharedKernel;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.VerificationStatus;

public class VerificationStatus : ValueObject
{
    public VerificationStatusEnum Value { get; }

    public VerificationStatus(VerificationStatusEnum value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}