using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;

public class UserPictureUpdatedEvent : DomainEvent
{
    public string Url { get; private set; }

    public UserPictureUpdatedEvent(int aggregateId, string url)
        : base(aggregateId)
    {
        Url = url;
    }
}