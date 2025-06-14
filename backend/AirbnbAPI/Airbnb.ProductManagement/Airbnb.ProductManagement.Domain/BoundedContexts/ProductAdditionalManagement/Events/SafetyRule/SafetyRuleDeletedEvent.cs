using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.SafetyRule;

public class SafetyRuleDeletedEvent : DomainEvent
{
    public int RuleId { get; }

    public SafetyRuleDeletedEvent(int aggregateId, int ruleId)
        : base(aggregateId)
    {
        RuleId = ruleId;
    }
}