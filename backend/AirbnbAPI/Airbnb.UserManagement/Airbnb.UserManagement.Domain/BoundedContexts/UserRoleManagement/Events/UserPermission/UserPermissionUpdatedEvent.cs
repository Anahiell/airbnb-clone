using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;

public class UserRoleUpdatedEvent : DomainEvent
{
    public int UserId { get; }
    public int OldRoleId { get; }
    public int NewRoleId { get; }

    public string Name { get; }
    public UserRoleUpdatedEvent(int userId, int oldRoleId, int newRoleId, string name = null)
        : base(userId)
    {
        UserId = userId;
        OldRoleId = oldRoleId;
        NewRoleId = newRoleId;
        Name = name;
    }
}