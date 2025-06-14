using Airbnb.SharedKernel;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Events;

public class DocumentVerificationUpdatedEvent : IDomainEvent
{
    public int AggregateId { get; }

    public DocumentVerificationUpdatedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}