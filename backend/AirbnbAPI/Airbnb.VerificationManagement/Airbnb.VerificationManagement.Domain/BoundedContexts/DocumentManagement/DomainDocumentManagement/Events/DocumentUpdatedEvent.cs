using Airbnb.SharedKernel;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.Events;


public class DocumentUpdatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public string FilePath { get; }
    public DateTime UpdatedAt { get; }

    public DocumentUpdatedEvent(int aggregateId, string filePath)
    {
        AggregateId = aggregateId;
        FilePath = filePath;
        UpdatedAt = DateTime.UtcNow;
    }
}