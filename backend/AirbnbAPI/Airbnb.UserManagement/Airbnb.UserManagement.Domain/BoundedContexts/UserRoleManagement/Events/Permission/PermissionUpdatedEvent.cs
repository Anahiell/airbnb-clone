using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Permission;

public class PermissionUpdatedEvent : DomainEvent
{
    public string Permission { get; }

    public PermissionUpdatedEvent(int aggregateId, string permission)
        : base(aggregateId)
    {
        Permission = permission;
    }
}