using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Permission;

public class PermissionDeletedEvent : DomainEvent
{
    public int PermissionId { get; private set; }

    public PermissionDeletedEvent(int permissionId) : base(permissionId)
    {
        PermissionId = permissionId;
    }
}