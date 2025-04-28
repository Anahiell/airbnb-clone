using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;

public class DomainUser : IdentityUser<int>
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime DateOfBirth { get; private set; }

    public DomainUser()
    {
    }

    public DomainUser(string fullName, string email, UserRole role, DateTime dateOfBirth)
    {
        FullName = fullName;
        Email = email;
        Role = role;
        DateOfBirth = dateOfBirth;

        RaiseEvent(new UserCreatedEvent(Id, fullName, email, role, dateOfBirth));
    }

    #region Aggregate Methods

    public void UpdateUser(string fullName, string email, UserRole role, DateTime dateOfBirth)
    {
        FullName = fullName;
        Email = email;
        Role = role;
        DateOfBirth = dateOfBirth;

        RaiseEvent(new UserUpdatedEvent(Id, fullName, email, role, dateOfBirth));
    }

    public void DeleteUser()
    {
        RaiseEvent(new UserDeletedEvent(Id));
    }

    #endregion

    #region Event Handling

    protected void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case UserCreatedEvent e:
                OnUserCreatedEvent(e);
                break;
            case UserUpdatedEvent e:
                OnUserUpdatedEvent(e);
                break;
            case UserDeletedEvent e:
                OnUserDeletedEvent(e);
                break;
        }
    }

    private void OnUserCreatedEvent(UserCreatedEvent @event)
    {
        Id = @event.AggregateId;
        FullName = @event.FullName;
        Email = @event.Email;
        Role = @event.Role;
        DateOfBirth = @event.DateOfBirth;
    }

    private void OnUserUpdatedEvent(UserUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        FullName = @event.FullName;
        Email = @event.Email;
        Role = @event.Role;
        DateOfBirth = @event.DateOfBirth;
    }

    private void OnUserDeletedEvent(UserDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion

    public void RaiseEvent(IDomainEvent @event)
    {
        When(@event);
    }
}

public enum UserRole
{
    Customer,
    Admin
}