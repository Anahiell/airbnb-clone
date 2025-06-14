using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;

public class UserRoleDeletedEvent : DomainEvent
{
    public int UserId { get; }
    public int RoleId { get; }

    public UserRoleDeletedEvent(int userId, int roleId)
        : base(userId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}