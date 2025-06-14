using Airbnb.SharedKernel;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.Events;

public class DriverLicenseDocumentCreatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public int UserId { get; }
    public string LicenseNumber { get; }
    public DateOnly ExpirationDate { get; }
    public DocumentType DocumentType { get; }
    public string FilePath { get; }
    public DateTime CreatedAt { get; }
    public string DataJson { get; }

    public DriverLicenseDocumentCreatedEvent(
        int aggregateId,
        int userId,
        string licenseNumber,
        DateOnly expirationDate,
        DocumentType documentType,
        string filePath,
        string dataJson)
    {
        AggregateId = aggregateId;
        UserId = userId;
        LicenseNumber = licenseNumber;
        ExpirationDate = expirationDate;
        DocumentType = documentType;
        FilePath = filePath;
        DataJson = dataJson;
        CreatedAt = DateTime.UtcNow;
    }
}