using Airbnb.SharedKernel;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Events;

public class DocumentVerificationDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }

    public DocumentVerificationDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}