using Airbnb.SharedKernel;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Events;

public class DocumentVerificationCreatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public int UserId { get; }
    public int DocumentId { get; }
    public DateTime SubmittedAt { get; }

    public DocumentVerificationCreatedEvent(int aggregateId, int userId, int documentId, DateTime submittedAt)
    {
        AggregateId = aggregateId;
        UserId = userId;
        DocumentId = documentId;
        SubmittedAt = submittedAt;
    }
}