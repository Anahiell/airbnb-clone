using Airbnb.SharedKernel;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.VerificationStatus;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Events;

public class DocumentVerificationStatusChangedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public VerificationStatus NewStatus { get; }
    public DateTime? VerifiedAt { get; }
    public string? FailureReason { get; }

    public DocumentVerificationStatusChangedEvent(int aggregateId, VerificationStatus newStatus, DateTime? verifiedAt = null, string? failureReason = null)
    {
        AggregateId = aggregateId;
        NewStatus = newStatus;
        VerifiedAt = verifiedAt;
        FailureReason = failureReason;
    }
}