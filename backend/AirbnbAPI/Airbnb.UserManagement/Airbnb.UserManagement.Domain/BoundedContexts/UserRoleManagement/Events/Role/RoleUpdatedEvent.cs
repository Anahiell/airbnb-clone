using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Role;

public class RoleUpdatedEvent : DomainEvent
{
    public string Name { get; private set; }

    public RoleUpdatedEvent(int aggregateId, string name)
        : base(aggregateId)
    {
        Name = name;
    }
}