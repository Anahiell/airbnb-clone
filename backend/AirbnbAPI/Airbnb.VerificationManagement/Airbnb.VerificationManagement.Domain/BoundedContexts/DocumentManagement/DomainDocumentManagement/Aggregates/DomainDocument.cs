using Airbnb.SharedKernel;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.Events;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;

public class DomainDocument<TDocumentData> : DocumentDataBase
    where TDocumentData : DocumentDataBase
{
    public TDocumentData DocumentData { get; private set; }

    public DomainDocument(int userId, DocumentType type, string filePath)
    {
        UserId = userId;
        Type = type;
        FilePath = filePath;
        UploadedAt = DateTime.UtcNow;

        RaiseEvent(new DocumentCreatedEvent(Id, userId, type, filePath));
    }
    
    public DomainDocument(
        int id,
        int userId,
        string filePath,
        DateTime uploadedAt,
        TDocumentData documentData
    )
    {
        Id = id;
        UserId = userId;
        FilePath = filePath;
        UploadedAt = uploadedAt;
        DocumentData = documentData;
        Type = documentData.Type;
    }
    
    public DomainDocument(
        int id,
        int userId,
        string filePath,
        DateTime uploadedAt,
        string documentData
    )
    {
        Id = id;
        UserId = userId;
        FilePath = filePath;
        UploadedAt = uploadedAt;
        DataJson = documentData;
    }

    public void UpdateFile(string newFilePath)
    {
        FilePath = newFilePath;
        RaiseEvent(new DocumentUpdatedEvent(Id, newFilePath));
    }
    
    public void Delete()
    {
        RaiseEvent(new DocumentDeletedEvent(Id));
    }
    

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case DocumentCreatedEvent e:
                OnDocumentCreated(e);
                break;
            case DocumentUpdatedEvent e:
                OnDocumentUpdated(e);
                break;
            case DocumentDeletedEvent e:
                OnDocumentDeleted(e);
                break;
        }
    }

    private void OnDocumentCreated(DocumentCreatedEvent e)
    {
        Id = e.AggregateId;
        UserId = e.UserId;
        Type = e.DocumentType;
        FilePath = e.FilePath;
        UploadedAt = e.CreatedAt;
    }

    private void OnDocumentUpdated(DocumentUpdatedEvent e)
    {
        FilePath = e.FilePath;
    }

    private void OnDocumentDeleted(DocumentDeletedEvent e)
    {
        Id = e.AggregateId;
    }
}