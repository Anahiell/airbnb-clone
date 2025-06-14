using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Permission;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

public class DomainPermission : AggregateRoot
{
    public string Permission { get; private set; }

    public DomainPermission(string permission)
    {
        Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        RaiseEvent(new PermissionCreatedEvent(Id, Permission));
    }

    public void UpdatePermission(string newPermission)
    {
        if (string.IsNullOrWhiteSpace(newPermission))
            throw new ArgumentException("Permission cannot be empty.");

        Permission = newPermission;
        RaiseEvent(new PermissionUpdatedEvent(Id, Permission));
    }

    public void Delete()
    {
        RaiseEvent(new PermissionDeletedEvent(Id));
    }

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case PermissionCreatedEvent e:
                Id = e.AggregateId;
                Permission = e.Permission;
                break;
            case PermissionUpdatedEvent e:
                Permission = e.Permission;
                break;
            case PermissionDeletedEvent:
                break;
        }
    }

    public void RaiseEvent(IDomainEvent @event)
    {
        When(@event);
    }

    private DomainPermission() { }
}