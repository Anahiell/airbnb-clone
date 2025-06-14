using Airbnb.SharedKernel;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.Events;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.ValueObjects;
using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.Aggregates;

public class PassportDocument : DocumentDataBase
{
    public PassportNumber Number { get; private set; }
    public CountryCode CountryCode { get; private set; }
    public Authority IssuedBy { get; private set; }
    public DateOnly IssuedDate { get; private set; }
    public DateOnly ExpirationDate { get; private set; }
    public override DocumentType Type => new DocumentType(DocumentTypeEnum.Passport);

    public PassportDocument(int userId, string filePath, DateTime uploadedAt, PassportNumber number, CountryCode countryCode,
        Authority issuedBy, DateOnly issuedDate, DateOnly expirationDate, string dataJson)
    {
        RaiseEvent(new PassportDocumentCreatedEvent(
            Id, userId, number, countryCode, issuedBy, issuedDate, expirationDate,
            new DocumentType(DocumentTypeEnum.Passport), filePath, dataJson));
    }

    public void Update(PassportNumber number, CountryCode countryCode, Authority issuedBy, DateOnly issuedDate,
        DateOnly expirationDate, string dataJson)
    {
        RaiseEvent(new PassportDocumentUpdatedEvent(
            Id, UserId, number, countryCode, issuedBy, issuedDate, expirationDate,
            new DocumentType(DocumentTypeEnum.Passport), FilePath, dataJson));
    }

    public void Delete()
    {
        RaiseEvent(new PassportDocumentDeletedEvent(Id));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case PassportDocumentCreatedEvent e:
                OnPassportDocumentCreatedEvent(e);
                break;
            case PassportDocumentUpdatedEvent e:
                OnPassportDocumentUpdatedEvent(e);
                break;
            case PassportDocumentDeletedEvent e:
                OnPassportDocumentDeletedEvent(e);
                break;
        }
    }

    private void OnPassportDocumentCreatedEvent(PassportDocumentCreatedEvent e)
    {
        Id = e.AggregateId;
        UserId = e.UserId;
        Number = e.PassportNumber;
        CountryCode = e.CountryCode;
        IssuedBy = e.IssuedBy;
        IssuedDate = e.IssuedDate;
        ExpirationDate = e.ExpirationDate;
        FilePath = e.FilePath;
        UploadedAt = e.CreatedAt;
        DataJson = e.DataJson;
    }

    private void OnPassportDocumentUpdatedEvent(PassportDocumentUpdatedEvent e)
    {
        Number = e.PassportNumber;
        CountryCode = e.CountryCode;
        IssuedBy = e.IssuedBy;
        IssuedDate = e.IssuedDate;
        ExpirationDate = e.ExpirationDate;
        DataJson = e.DataJson;
    }

    private void OnPassportDocumentDeletedEvent(PassportDocumentDeletedEvent e)
    {
        
    }

    #endregion
}