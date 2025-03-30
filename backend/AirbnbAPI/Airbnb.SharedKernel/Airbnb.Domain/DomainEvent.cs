using MediatR;

namespace Airbnb.SharedKernel;

public class DomainEvent : IDomainEvent, INotification
{
    public int EventId { get; private set; }
    public int AggregateId { get; protected set; }

    protected DomainEvent()
    {
    }

    protected DomainEvent(int aggregateId) : this()
    {
        AggregateId = aggregateId;
    }
}