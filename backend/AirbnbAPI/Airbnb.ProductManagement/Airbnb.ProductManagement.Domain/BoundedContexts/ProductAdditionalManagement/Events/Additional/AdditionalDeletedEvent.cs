using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.Additional;

public class AdditionalDeletedEvent : DomainEvent
{
    public AdditionalDeletedEvent(int aggregateId)
        : base(aggregateId)
    {
    }
}