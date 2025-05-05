using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;

public class UserPictureArchivedEvent : DomainEvent
{
    public UserPictureArchivedEvent(int aggregateId)
        : base(aggregateId)
    {

    }
}