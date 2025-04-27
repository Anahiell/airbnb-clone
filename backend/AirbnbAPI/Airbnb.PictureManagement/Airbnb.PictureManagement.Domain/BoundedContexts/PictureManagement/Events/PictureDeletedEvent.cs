using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;

public class PictureDeletedEvent : DomainEvent
{
    public PictureDeletedEvent(int aggregateId)
        : base(aggregateId)
    {
    }
}