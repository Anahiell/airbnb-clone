using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;

public class UserPictureDeletedEvent : DomainEvent
{
    public UserPictureDeletedEvent(int aggregateId)
        : base(aggregateId)
    {
    }
}