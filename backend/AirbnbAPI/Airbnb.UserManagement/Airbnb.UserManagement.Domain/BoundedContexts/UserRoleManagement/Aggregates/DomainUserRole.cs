using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

public class DomainUserRole : AggregateRoot
{
    public int UserId { get; private set; }
    public int RoleId { get; private set; }

    private DomainUserRole() { }

    public DomainUserRole(int userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;

        RaiseEvent(new UserRoleCreatedEvent(Id, userId, roleId));
    }

    public void UpdateRole(int newRoleId)
    {
        if (RoleId == newRoleId) 
            return;

        var oldRoleId = RoleId;
        RaiseEvent(new UserRoleUpdatedEvent(UserId, oldRoleId, newRoleId));
    }

    public void Remove()
    {
        RaiseEvent(new UserRoleDeletedEvent(UserId, RoleId));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case UserRoleCreatedEvent e:
                OnUserRoleCreated(e);
                break;

            case UserRoleUpdatedEvent e:
                OnUserRoleUpdated(e);
                break;

            case UserRoleDeletedEvent e:
                OnUserRoleRemoved(e);
                break;
        }
    }

    private void OnUserRoleCreated(UserRoleCreatedEvent e)
    {
        UserId = e.UserId;
        RoleId = e.RoleId;
    }

    private void OnUserRoleUpdated(UserRoleUpdatedEvent e)
    {
        RoleId = e.NewRoleId;
    }

    private void OnUserRoleRemoved(UserRoleDeletedEvent e)
    {
        
    }

    #endregion

    private void RaiseEvent(IDomainEvent @event)
    {
        When(@event);
    }
}