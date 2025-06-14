using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;

public class UserPermissionCreatedEvent : DomainEvent
{
    public int UserId { get; }
    public int PermissionId { get; }

    public string Name { get; }
    public UserPermissionCreatedEvent(int id, int userId, int permissionId, string? name = null)
        : base(userId)
    {
        AggregateId = id;
        UserId = userId;
        PermissionId = permissionId;
        Name = name;
    }
}