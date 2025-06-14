using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;

public class UserPermissionUpdatedEvent : DomainEvent
{
    public int Id { get; }
    public int UserId { get; }
    public int NewPermissionId { get; }
    public string Name { get; }

    public UserPermissionUpdatedEvent(int id, int userId, int newPermissionId, string name = null)
        : base(id)
    {
        Id = id;
        UserId = userId;
        NewPermissionId = newPermissionId;
        Name = name;
    }
}