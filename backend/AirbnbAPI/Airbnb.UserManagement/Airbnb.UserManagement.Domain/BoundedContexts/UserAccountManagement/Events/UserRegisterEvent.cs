using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;

public class UserRegisterEvent : DomainEvent
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }
    public ICollection<DomainUserRole> Roles { get; private set; }
    public ICollection<DomainUserPermission> Permissions { get; private set; }
    public DateTime DateOfBirth { get; private set; }

    public UserRegisterEvent(int aggregateId, string fullName, string username, string email, ICollection<DomainUserRole> roles, ICollection<DomainUserPermission> permissions)
        : base(aggregateId)
    {
        FullName = fullName;
        Username = username;
        Email = email;
        Roles = roles;
        Permissions = permissions;
    }
}