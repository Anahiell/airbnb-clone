using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;

public class UserCreatedEvent : DomainEvent
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime DateOfBirth { get; private set; }

    public UserCreatedEvent(int aggregateId, string fullName, string email, UserRole role, DateTime dateOfBirth)
        : base(aggregateId)
    {
        FullName = fullName;
        Email = email;
        Role = role;
        DateOfBirth = dateOfBirth;
    }
}