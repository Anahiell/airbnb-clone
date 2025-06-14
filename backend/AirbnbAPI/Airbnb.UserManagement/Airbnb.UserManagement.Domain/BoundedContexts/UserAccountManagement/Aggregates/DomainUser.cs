using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.ValueObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;

public class DomainUser : IdentityUser<int>
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string PasswordHash { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public bool IsDocumentVerified { get; private set; }
    public ICollection<DomainUserLanguage> Languages { get; private set; } = new List<DomainUserLanguage>();
    public UserProfile? Profile { get; private set; }
    public ICollection<DomainUserRole> UserRoles { get; private set; } = new List<DomainUserRole>();
    public ICollection<DomainUserPermission> UserPermissions { get; private set; } = new List<DomainUserPermission>();
    public override string UserName { get; set; }

    public void UpdatePermissions(IEnumerable<int> permissionIds)
    {
        ArgumentNullException.ThrowIfNull(permissionIds);

        UserPermissions.Clear();

        foreach (var languageId in permissionIds.Distinct())
        {
            UserPermissions.Add(new DomainUserPermission(Id, languageId));
        }
    }
    
    public void UpdateRoles(IEnumerable<int> roleIds)
    {
        ArgumentNullException.ThrowIfNull(roleIds);

        UserRoles.Clear();

        foreach (var languageId in roleIds.Distinct())
        {
            UserRoles.Add(new DomainUserRole(Id, languageId));
        }
    }
    
    public void UpdateLanguages(IEnumerable<int> languageIds)
    {
        ArgumentNullException.ThrowIfNull(languageIds);

        Languages.Clear();

        foreach (var languageId in languageIds.Distinct())
        {
            Languages.Add(new DomainUserLanguage(Id, languageId));
        }
    }

    public void UpdateProfile(UserProfile? newProfile)
    {
        Profile = newProfile;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }

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

    public DomainUser(string fullName, string username, string email, DateTime dateOfBirth)
    {
        FullName = fullName;
        Email = email;
        UserRoles = new List<DomainUserRole>();
        UserPermissions = new List<DomainUserPermission>();
        DateOfBirth = dateOfBirth;
        UserName = username;
        
        RaiseEvent(new UserCreatedEvent(Id, fullName, UserName, email, UserRoles.ToList(), UserPermissions.ToList(), dateOfBirth));
    }

    #region Aggregate Methods

    public void UpdateUser(string fullName, string email, DateTime dateOfBirth)
    {
        FullName = fullName;
        Email = email;
        DateOfBirth = dateOfBirth;

        RaiseEvent(new UserUpdatedEvent(Id, fullName, email, dateOfBirth, Profile, UserRoles.ToList(), UserPermissions.ToList(), Languages.ToList()));
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
            case UserRegisterEvent e:
                OnUserRegisterEvent(e);
                break;
        }
    }

    private void OnUserCreatedEvent(UserCreatedEvent @event)
    {
        Id = @event.AggregateId;
        FullName = @event.FullName;
        Email = @event.Email;
        UserRoles = @event.Roles;
        UserPermissions = @event.Permissions;
        DateOfBirth = @event.DateOfBirth;
    }

    private void OnUserRegisterEvent(UserRegisterEvent @event)
    {
        Id = @event.AggregateId;
        FullName = @event.FullName;
        Email = @event.Email;
        UserRoles = @event.Roles;
        UserPermissions = @event.Permissions;
        DateOfBirth = @event.DateOfBirth;
    }

    private void OnUserUpdatedEvent(UserUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        FullName = @event.FullName;
        Email = @event.Email;
        UserRoles = @event.Roles;
        UserPermissions = @event.Permissions;
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