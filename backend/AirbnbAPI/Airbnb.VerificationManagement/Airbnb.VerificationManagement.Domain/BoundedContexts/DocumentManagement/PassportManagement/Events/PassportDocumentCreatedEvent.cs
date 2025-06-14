using Airbnb.SharedKernel;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.ValueObjects;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.Events;

public class PassportDocumentCreatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public int UserId { get; }
    public PassportNumber PassportNumber { get; }
    public CountryCode CountryCode { get; }
    public Authority IssuedBy { get; }
    public DateOnly IssuedDate { get; }
    public DateOnly ExpirationDate { get; }
    public DocumentType DocumentType { get; }
    public string FilePath { get; }
    public DateTime CreatedAt { get; }
    public string DataJson { get; }

    public PassportDocumentCreatedEvent(
        int aggregateId,
        int userId,
        PassportNumber passportNumber,
        CountryCode countryCode,
        Authority issuedBy,
        DateOnly issuedDate,
        DateOnly expirationDate,
        DocumentType documentType,
        string filePath, string dataJson)
    {
        AggregateId = aggregateId;
        UserId = userId;
        PassportNumber = passportNumber;
        CountryCode = countryCode;
        IssuedBy = issuedBy;
        IssuedDate = issuedDate;
        ExpirationDate = expirationDate;
        DocumentType = documentType;
        FilePath = filePath;
        DataJson = dataJson;
        CreatedAt = DateTime.UtcNow;
    }
}