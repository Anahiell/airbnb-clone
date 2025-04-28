using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;

public class UserDeletedEvent : DomainEvent
{
    public int Id { get; private set; }

    public UserDeletedEvent(int aggregateId)
        : base(aggregateId)
    {
        Id = aggregateId;
    }
}