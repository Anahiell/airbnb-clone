using Airbnb.SharedKernel;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.Events;

public class DriverLicenseDocumentDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public DateTime DeletedAt { get; }

    public DriverLicenseDocumentDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
        DeletedAt = DateTime.UtcNow;
    }
}