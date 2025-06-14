using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Role;

public class RoleDeletedEvent : DomainEvent
{
    public RoleDeletedEvent(int aggregateId) : base(aggregateId) { }
}