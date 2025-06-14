using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.CancelPolicy;

public class CancelPolicyCreatedEvent : DomainEvent
{
    public int FreeCancelationDays { get; }
    public int PartCancelationDays { get; }
    public int PartCancelationPercent { get; }

    public CancelPolicyCreatedEvent(int aggregateId, ValueObjects.CancelPolicy cancelPolicy)
        : base(aggregateId)
    {
        FreeCancelationDays = cancelPolicy.FreeCancelationDays;
        PartCancelationDays = cancelPolicy.PartCancelationDays;
        PartCancelationPercent = cancelPolicy.PartCancelationPercent;
    }
}