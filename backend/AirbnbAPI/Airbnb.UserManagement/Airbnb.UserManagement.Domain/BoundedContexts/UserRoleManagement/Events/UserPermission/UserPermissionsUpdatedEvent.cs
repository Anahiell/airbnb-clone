using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;

public class UserPermissionsUpdatedEvent : DomainEvent
{
    public int UserId { get; }
    public List<string?>? PermissionsNames { get; }

    public UserPermissionsUpdatedEvent(int userId, List<string?>? permissionsNames)
    {
        UserId = userId;
        PermissionsNames = permissionsNames;
    }
}