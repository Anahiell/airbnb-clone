using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.SafetyRule;

public class SafetyRuleUpdatedEvent : DomainEvent
{
    public int RuleId { get; }
    public string Type { get; }
    public string Label { get; }

    public SafetyRuleUpdatedEvent(int aggregateId, int ruleId, string type, string label)
        : base(aggregateId)
    {
        RuleId = ruleId;
        Type = type;
        Label = label;
    }
}