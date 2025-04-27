using Airbnb.SharedKernel;

namespace Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Events;

public class TagCreatedEvent : DomainEvent
{
    public string Name { get; }
    public DateTime CreatedAt { get; }

    public TagCreatedEvent(int aggregateId, string name, DateTime createdAt) 
        : base(aggregateId)
    {
        Name = name;
        CreatedAt = createdAt;
    }
}