using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.CancelPolicy;

public class CancelPolicyDeletedEvent : DomainEvent
{
    public CancelPolicyDeletedEvent(int aggregateId)
        : base(aggregateId)
    {
    }
}