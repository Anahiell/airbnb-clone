using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;

public class UserRoleCreatedEvent : DomainEvent
{
    public int Id { get; }
    public int UserId { get; }
    public int RoleId { get; }
    public string? Name { get; }

    public UserRoleCreatedEvent(int id, int userId, int roleId, string? name = null)
        : base(userId)
    {
        Id = id;
        UserId = userId;
        RoleId = roleId;
        Name = name;
    }
}