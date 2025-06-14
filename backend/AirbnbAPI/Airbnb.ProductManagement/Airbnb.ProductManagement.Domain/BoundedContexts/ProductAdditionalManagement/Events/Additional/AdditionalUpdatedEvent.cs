using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.ValueObjects;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.Additional;

public class AdditionalUpdatedEvent : DomainEvent
{
    public ValueObjects.CancelPolicy CancelPolicy { get; }
    public IEnumerable<ValueObjects.HomeRule> HomeRules { get; }
    public IEnumerable<ValueObjects.SafetyRule> SafetyRules { get; }

    public AdditionalUpdatedEvent(int aggregateId, ValueObjects.CancelPolicy cancelPolicy,
        IEnumerable<ValueObjects.HomeRule> homeRules, IEnumerable<ValueObjects.SafetyRule> safetyRules)
        : base(aggregateId)
    {
        CancelPolicy = cancelPolicy;
        HomeRules = homeRules;
        SafetyRules = safetyRules;
    }
}