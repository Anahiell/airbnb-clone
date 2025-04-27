using Airbnb.SharedKernel;

namespace Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Events;

public class TagUpdatedEvent : DomainEvent
{
    public string NewName { get; }

    public TagUpdatedEvent(int aggregateId, string newName)
        : base(aggregateId)
    {
        NewName = newName;
    }
}