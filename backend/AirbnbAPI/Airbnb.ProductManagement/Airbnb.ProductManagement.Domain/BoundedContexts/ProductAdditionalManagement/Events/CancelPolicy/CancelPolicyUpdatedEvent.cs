using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.CancelPolicy;

public class CancelPolicyUpdatedEvent : DomainEvent
{
    public int FreeCancelationDays { get; }
    public int PartCancelationDays { get; }
    public int PartCancelationPercent { get; }

    public CancelPolicyUpdatedEvent(int aggregateId, ValueObjects.CancelPolicy cancelPolicy)
        : base(aggregateId)
    {
        FreeCancelationDays = cancelPolicy.FreeCancelationDays;
        PartCancelationDays = cancelPolicy.PartCancelationDays;
        PartCancelationPercent = cancelPolicy.PartCancelationPercent;
    }
}