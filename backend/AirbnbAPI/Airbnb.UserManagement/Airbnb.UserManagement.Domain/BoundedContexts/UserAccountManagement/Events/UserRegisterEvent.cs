using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;

public class UserRegisterEvent : DomainEvent
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public List<UserRole> Roles { get; private set; } = new();
    public DateTime DateOfBirth { get; private set; }

    public UserRegisterEvent(int aggregateId, string fullName, string email)
        : base(aggregateId)
    {
        FullName = fullName;
        Email = email;
    }
}