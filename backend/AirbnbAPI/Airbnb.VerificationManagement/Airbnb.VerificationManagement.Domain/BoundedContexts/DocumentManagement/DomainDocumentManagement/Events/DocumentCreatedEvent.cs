using Airbnb.SharedKernel;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.Events;

public class DocumentCreatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public int UserId { get; }
    public DocumentType DocumentType { get; }
    public string FilePath { get; }
    public DateTime CreatedAt { get; }

    public DocumentCreatedEvent(int aggregateId, int userId, DocumentType documentType, string filePath)
    {
        AggregateId = aggregateId;
        UserId = userId;
        DocumentType = documentType;
        FilePath = filePath;
        CreatedAt = DateTime.UtcNow;
    }
}