using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;

public class LanguageCreatedEvent : DomainEvent
{
    public string Name { get; }
    public DateTime CreatedAt { get; }

    public LanguageCreatedEvent(int aggregateId, string name, DateTime createdAt)
        : base(aggregateId)
    {
        Name = name;
        CreatedAt = createdAt;
    }
}