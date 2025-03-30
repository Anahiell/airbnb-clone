using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.AddressManagement.Events;

public class AddressLegalDeletedEvent(int aggregateId) : DomainEvent(aggregateId)
{
    public int AddressLegalId { get; private set; } = aggregateId;
}