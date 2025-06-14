using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Role;

public class DomainRole : AggregateRoot
{
    public string Name { get; private set; }

    public DomainRole(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be null or empty.");

        Name = name;
        RaiseEvent(new RoleCreatedEvent(Id, name));
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("New name cannot be null or empty.");

        Name = newName;
        RaiseEvent(new RoleUpdatedEvent(Id, Name));
    }

    public void Delete()
    {
        RaiseEvent(new RoleDeletedEvent(Id));
    }

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case RoleCreatedEvent e:
                Id = e.AggregateId;
                Name = e.Name;
                break;

            case RoleUpdatedEvent e:
                Name = e.Name;
                break;
        }
    }

    private void RaiseEvent(IDomainEvent @event)
    {
        When(@event);
    }

    private DomainRole() { }
}