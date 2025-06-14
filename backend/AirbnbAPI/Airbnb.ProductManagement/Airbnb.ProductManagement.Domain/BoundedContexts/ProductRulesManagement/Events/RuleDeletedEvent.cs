using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRulesManagement.Events;

public class RuleDeletedEvent : IDomainEvent
{
    public int AggregateId { get; }

    public RuleDeletedEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}