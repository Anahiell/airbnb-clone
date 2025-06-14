using Airbnb.SharedKernel;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.Events;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;
using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.Aggregates;

public class DriverLicenseDocument : DocumentDataBase
{
    public string LicenseNumber { get; private set; }
    public DateOnly ExpirationDate { get; private set; }

    public override DocumentType Type => new DocumentType(DocumentTypeEnum.DriverLicense);

    public DriverLicenseDocument(
        int userId,
        string filePath,
        DateTime uploadedAt,
        string licenseNumber,
        DateOnly expirationDate,
        string dataJson)
    {
        RaiseEvent(new DriverLicenseDocumentCreatedEvent(
            Id,
            userId,
            licenseNumber,
            expirationDate,
            new DocumentType(DocumentTypeEnum.DriverLicense),
            filePath,
            dataJson
        ));
    }

    public void Update(string licenseNumber, DateOnly expirationDate, string dataJson)
    {
        RaiseEvent(new DriverLicenseDocumentUpdatedEvent(
            Id,
            UserId,
            licenseNumber,
            expirationDate,
            new DocumentType(DocumentTypeEnum.DriverLicense),
            FilePath,
            dataJson
        ));
    }

    public void Delete()
    {
        RaiseEvent(new DriverLicenseDocumentDeletedEvent(Id));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case DriverLicenseDocumentCreatedEvent e:
                OnDriverLicenseDocumentCreatedEvent(e);
                break;
            case DriverLicenseDocumentUpdatedEvent e:
                OnDriverLicenseDocumentUpdatedEvent(e);
                break;
            case DriverLicenseDocumentDeletedEvent e:
                OnDriverLicenseDocumentDeletedEvent(e);
                break;
        }
    }

    private void OnDriverLicenseDocumentCreatedEvent(DriverLicenseDocumentCreatedEvent e)
    {
        Id = e.AggregateId;
        UserId = e.UserId;
        LicenseNumber = e.LicenseNumber;
        ExpirationDate = e.ExpirationDate;
        FilePath = e.FilePath;
        UploadedAt = e.CreatedAt;
        DataJson = e.DataJson;
    }

    private void OnDriverLicenseDocumentUpdatedEvent(DriverLicenseDocumentUpdatedEvent e)
    {
        LicenseNumber = e.LicenseNumber;
        ExpirationDate = e.ExpirationDate;
        DataJson = e.DataJson;
    }

    private void OnDriverLicenseDocumentDeletedEvent(DriverLicenseDocumentDeletedEvent e)
    {
        
    }

    #endregion
}