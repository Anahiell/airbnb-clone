using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;

public class UserCreatedEvent : DomainEvent
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public List<UserRole> Roles { get; private set; } = new();
    public DateTime DateOfBirth { get; private set; }

    public UserCreatedEvent(int aggregateId, string fullName, string email, List<UserRole> roles, DateTime dateOfBirth)
        : base(aggregateId)
    {
        FullName = fullName;
        Email = email;
        Roles = roles;
        DateOfBirth = dateOfBirth;
    }
}