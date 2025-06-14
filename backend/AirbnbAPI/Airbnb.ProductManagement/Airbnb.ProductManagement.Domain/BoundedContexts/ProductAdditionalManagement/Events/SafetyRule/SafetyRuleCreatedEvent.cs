using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.SafetyRule;

public class SafetyRuleCreatedEvent : DomainEvent
{
    public int RuleId { get; }
    public string Type { get; }
    public string Label { get; }

    public SafetyRuleCreatedEvent(int aggregateId, int ruleId, string type, string label)
        : base(aggregateId)
    {
        RuleId = ruleId;
        Type = type;
        Label = label;
    }
}