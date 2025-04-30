using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;

public class DomainUser : IdentityUser<int>
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public List<UserRole> Roles { get; private set; } = new();
    public DateTime DateOfBirth { get; private set; }
    public string PasswordHash { get; private set; }

    public bool CheckPassword(string password)
    {
        var hasher = new PasswordHasher<DomainUser>();
        var result = hasher.VerifyHashedPassword(this, PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }
    
    public void SetPassword(string password)
    {
        var hasher = new PasswordHasher<DomainUser>();
        PasswordHash = hasher.HashPassword(this, password);
    }
    
    public DomainUser()
    {
    }

    public DomainUser(string fullName, string email, List<UserRole> roles, DateTime dateOfBirth)
    {
        FullName = fullName;
        Email = email;
        Roles  = roles;
        DateOfBirth = dateOfBirth;

        RaiseEvent(new UserCreatedEvent(Id, fullName, email, roles, dateOfBirth));
    }

    #region Aggregate Methods

    public void UpdateUser(string fullName, string email, List<UserRole> roles, DateTime dateOfBirth)
    {
        FullName = fullName;
        Email = email;
        Roles = roles;
        DateOfBirth = dateOfBirth;

        RaiseEvent(new UserUpdatedEvent(Id, fullName, email, roles, dateOfBirth));
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
        Roles = @event.Roles;
        DateOfBirth = @event.DateOfBirth;
    }

    private void OnUserUpdatedEvent(UserUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        FullName = @event.FullName;
        Email = @event.Email;
        Roles = @event.Roles;
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