using Airbnb.SharedKernel;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.Events;

public class DocumentDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public DateTime DeletedAt { get; }

    public DocumentDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
        DeletedAt = DateTime.UtcNow;
    }
}