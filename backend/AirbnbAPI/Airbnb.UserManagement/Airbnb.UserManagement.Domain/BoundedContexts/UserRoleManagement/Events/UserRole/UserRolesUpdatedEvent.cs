using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;

public class UserRolesUpdatedEvent : DomainEvent
{
    public int UserId { get; }
    public List<string?>? RolesNames { get; }

    public UserRolesUpdatedEvent(int userId, List<string?>? rolesNames)
    {
        UserId = userId;
        RolesNames = rolesNames;
    }
}