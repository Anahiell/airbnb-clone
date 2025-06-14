using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.ValueObjects;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.Additional;

public class AdditionalCreatedEvent : DomainEvent
{
    public ValueObjects.CancelPolicy? CancelPolicy { get; }
    public IEnumerable<ValueObjects.HomeRule> HomeRules { get; }
    public IEnumerable<ValueObjects.SafetyRule> SafetyRules { get; }
    public int ProductId { get; }
    
    public AdditionalCreatedEvent(int aggregateId, int productId, ValueObjects.CancelPolicy cancelPolicy,
        IEnumerable<ValueObjects.HomeRule> homeRules, IEnumerable<ValueObjects.SafetyRule> safetyRules)
        : base(aggregateId)
    {
        ProductId = productId;
        CancelPolicy = cancelPolicy;
        HomeRules = homeRules;
        SafetyRules = safetyRules;
    }
}