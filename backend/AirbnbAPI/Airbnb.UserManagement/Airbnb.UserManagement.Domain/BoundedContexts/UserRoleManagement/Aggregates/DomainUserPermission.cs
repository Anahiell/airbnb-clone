using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

public class DomainUserPermission : AggregateRoot
{
    public int UserId { get; private set; }
    public int PermissionId { get; private set; }

    private DomainUserPermission() { }

    public DomainUserPermission(int userId, int permissionId)
    {
        UserId = userId;
        PermissionId = permissionId;

        RaiseEvent(new UserPermissionCreatedEvent(Id, userId, permissionId));
    }

    public void UpdatePermission(int newPermissionId)
    {
        if (PermissionId == newPermissionId)
        {
            return;
        }
        
        PermissionId = newPermissionId;
        
        RaiseEvent(new UserPermissionUpdatedEvent(Id, UserId, newPermissionId));
    }

    public void Remove()
    {
        RaiseEvent(new UserPermissionDeletedEvent(Id));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case UserPermissionCreatedEvent e:
                OnUserPermissionCreated(e);
                break;

            case UserPermissionUpdatedEvent e:
                OnUserPermissionUpdated(e);
                break;

            case UserPermissionDeletedEvent e:
                OnUserPermissionRemoved(e);
                break;
        }
    }

    private void OnUserPermissionCreated(UserPermissionCreatedEvent e)
    {
        UserId = e.UserId;
        PermissionId = e.PermissionId;
    }

    private void OnUserPermissionUpdated(UserPermissionUpdatedEvent e)
    {
        PermissionId = e.NewPermissionId;
    }

    private void OnUserPermissionRemoved(UserPermissionDeletedEvent e)
    {
        // Можно поставить флаг удаления или очистить поля
    }

    #endregion

    private void RaiseEvent(IDomainEvent @event)
    {
        When(@event);
    }
}