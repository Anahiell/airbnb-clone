using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.ValueObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;

public class UserUpdatedEvent : DomainEvent
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public bool IsDocumentVerified  { get; private set; }
    public bool IsEmailVerified  { get; private set; }
    public UserProfile? Profile { get; private set; }
    public List<DomainUserRole> Roles { get; private set; } = new();
    public List<DomainUserPermission> Permissions { get; private set; } = new();
    public List<DomainUserLanguage> Languages { get; private set; } = new();
    public DateTime DateOfBirth { get; private set; }

    public UserUpdatedEvent(int aggregateId, string fullName, string email, DateTime dateOfBirth, UserProfile? profile,
        List<DomainUserRole> roles = null, List<DomainUserPermission> permissions = null, List<DomainUserLanguage> languages = null)
        : base(aggregateId)
    {
        FullName = fullName;
        Email = email;
        DateOfBirth = dateOfBirth;
        Profile = profile;
        Languages = languages;
        Roles = roles;
        Permissions = permissions;
    }
}