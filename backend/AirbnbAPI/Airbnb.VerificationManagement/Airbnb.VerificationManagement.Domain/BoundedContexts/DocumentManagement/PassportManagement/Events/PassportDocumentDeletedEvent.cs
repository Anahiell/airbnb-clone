using Airbnb.SharedKernel;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.Events;

public class PassportDocumentDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public DateTime DeletedAt { get; }

    public PassportDocumentDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
        DeletedAt = DateTime.UtcNow;
    }
}