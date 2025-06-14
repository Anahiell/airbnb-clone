using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;

public class UserCreatedEvent : DomainEvent
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }
    public bool IsDocumentVerified  { get; private set; }
    public bool IsEmailVerified  { get; private set; }
    public List<DomainUserRole> Roles { get; private set; } = new();
    public List<DomainUserPermission> Permissions { get; private set; } = new();
    public DateTime DateOfBirth { get; private set; }

    public UserCreatedEvent(int aggregateId, string fullName, string username, string email, List<DomainUserRole> roles, List<DomainUserPermission> permissions, DateTime dateOfBirth)
        : base(aggregateId)
    {
        FullName = fullName;
        Username = username;
        Email = email;
        Roles = roles;
        Permissions = permissions;
        DateOfBirth = dateOfBirth;
        IsEmailVerified = false;
        IsDocumentVerified = false;
    }
}