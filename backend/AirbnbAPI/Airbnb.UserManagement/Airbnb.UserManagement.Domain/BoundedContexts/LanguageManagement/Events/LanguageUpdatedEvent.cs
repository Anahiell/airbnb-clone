using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;


public class LanguageUpdatedEvent : DomainEvent
{
    public string NewName { get; }

    public LanguageUpdatedEvent(int aggregateId, string newName)
        : base(aggregateId)
    {
        NewName = newName;
    }
}