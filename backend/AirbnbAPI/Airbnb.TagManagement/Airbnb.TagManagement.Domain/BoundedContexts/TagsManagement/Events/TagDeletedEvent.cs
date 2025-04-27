using Airbnb.SharedKernel;

namespace Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Events;

public class TagDeletedEvent : DomainEvent
{
    public TagDeletedEvent(int aggregateId)
        : base(aggregateId)
    {
    }
}