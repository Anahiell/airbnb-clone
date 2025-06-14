using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Permission;

public class PermissionCreatedEvent : DomainEvent
{
    public string Permission { get; }

    public PermissionCreatedEvent(int aggregateId, string permission)
        : base(aggregateId)
    {
        Permission = permission;
    }
}