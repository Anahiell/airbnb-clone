using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Role;

public class RoleCreatedEvent : DomainEvent
{
    public string Name { get; private set; }

    public RoleCreatedEvent(int aggregateId, string name)
        : base(aggregateId)
    {
        Name = name;
    }
}