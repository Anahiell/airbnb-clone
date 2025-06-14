using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.HomeRule;

public class HomeRuleDeletedEvent : DomainEvent
{
    public int RuleId { get; }

    public HomeRuleDeletedEvent(int aggregateId, int ruleId)
        : base(aggregateId)
    {
        RuleId = ruleId;
    }
}