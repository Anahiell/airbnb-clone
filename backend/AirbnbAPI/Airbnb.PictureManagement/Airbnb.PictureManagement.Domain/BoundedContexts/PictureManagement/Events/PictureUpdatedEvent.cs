using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;

public class PictureUpdatedEvent : DomainEvent
{
    public string Url { get; private set; }

    public PictureUpdatedEvent(int aggregateId, string url)
        : base(aggregateId)
    {
        Url = url;
    }
}