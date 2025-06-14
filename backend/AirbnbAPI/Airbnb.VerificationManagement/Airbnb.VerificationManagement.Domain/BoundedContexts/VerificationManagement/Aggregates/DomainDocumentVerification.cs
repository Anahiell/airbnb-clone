using Airbnb.SharedKernel;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.VerificationStatus;
using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Events;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;

public class DomainDocumentVerification : AggregateRoot
{
    public int UserId { get; private set; }
    public int DocumentId { get; private set; }
    public VerificationStatus Status { get; private set; }

    public DateTime SubmittedAt { get; private set; }

    public DateTime? VerifiedAt { get; private set; }

    public DomainDocumentVerification(int userId, int documentId)
    {
        UserId = userId;
        Status = new VerificationStatus(VerificationStatusEnum.Pending);
        SubmittedAt = DateTime.UtcNow;

        RaiseEvent(new DocumentVerificationCreatedEvent(Id, userId, documentId, DateTime.Now));
    }

    public void ChangeStatus(VerificationStatus newStatus, string? reason = null)
    {
        if (Status.Value != VerificationStatusEnum.Pending)
            throw new InvalidOperationException("Status can only be changed from Pending.");

        DateTime? verifiedAt = null;
        if (newStatus.Value == VerificationStatusEnum.Verified)
        {
            verifiedAt = DateTime.UtcNow;
        }

        RaiseEvent(new DocumentVerificationStatusChangedEvent(Id, newStatus, verifiedAt, reason));
    }

    public void Delete()
    {
        RaiseEvent(new DocumentVerificationDeletedEvent(Id));
    }

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case DocumentVerificationCreatedEvent e:
                OnDocumentVerificationCreated(e);
                break;
            case DocumentVerificationStatusChangedEvent e:
                OnDocumentVerificationStatusChanged(e);
                break;
            case DocumentVerificationDeletedEvent e:
                OnDocumentVerificationDeleted(e);
                break;
        }
    }

    private void OnDocumentVerificationCreated(DocumentVerificationCreatedEvent e)
    {
        Id = e.AggregateId;
        UserId = e.UserId;
        Status = new VerificationStatus(VerificationStatusEnum.Pending);
        SubmittedAt = e.SubmittedAt;
    }

    private void OnDocumentVerificationStatusChanged(DocumentVerificationStatusChangedEvent e)
    {
        Status = new VerificationStatus(VerificationStatusEnum.Pending);
        VerifiedAt = e.VerifiedAt;
    }

    private void OnDocumentVerificationDeleted(DocumentVerificationDeletedEvent e)
    {
        Id = e.AggregateId;
    }
}