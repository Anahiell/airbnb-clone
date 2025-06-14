using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.HomeRule;

public class HomeRuleUpdatedEvent : DomainEvent
{
    public int RuleId { get; }
    public string Type { get; }
    public string Text { get; }

    public HomeRuleUpdatedEvent(int aggregateId, int ruleId, string type, string text)
        : base(aggregateId)
    {
        RuleId = ruleId;
        Type = type;
        Text = text;
    }
}