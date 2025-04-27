using Airbnb.SharedKernel;

namespace Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Events;

public class ReviewDeletedEvent : DomainEvent
{
    public int Id { get; private set; }

    public ReviewDeletedEvent(int aggregateId)
        : base(aggregateId)
    {
        Id = aggregateId;
    }
}