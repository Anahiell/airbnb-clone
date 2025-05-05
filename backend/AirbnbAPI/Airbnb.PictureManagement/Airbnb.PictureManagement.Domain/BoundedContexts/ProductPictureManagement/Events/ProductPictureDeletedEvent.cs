using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;

public class ProductPictureDeletedEvent : DomainEvent
{
    public int Id { get; }
    public Guid AggregateId { get; }

    public ProductPictureDeletedEvent(int id, Guid aggregateId)
    {
        Id = id;
        AggregateId = aggregateId;
    }
}