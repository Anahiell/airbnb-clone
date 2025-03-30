using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ApartmentTypeManagement.Events;

public class ApartmentTypeDeletedEvent(int aggregateId) : DomainEvent(aggregateId)
{
    public int ApartmentTypeId { get; private set; } = aggregateId;
}