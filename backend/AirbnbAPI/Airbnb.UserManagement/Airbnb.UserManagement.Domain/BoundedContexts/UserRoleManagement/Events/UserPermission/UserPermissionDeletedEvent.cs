using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;

public class UserPermissionDeletedEvent : DomainEvent
{
    public UserPermissionDeletedEvent(int permissionId)
        : base(permissionId)
    {
        AggregateId = permissionId;
    }
}