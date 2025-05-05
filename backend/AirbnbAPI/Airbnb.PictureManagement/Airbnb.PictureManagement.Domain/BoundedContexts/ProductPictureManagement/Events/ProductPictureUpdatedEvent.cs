using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;

public class ProductPictureUpdatedEvent : DomainEvent
{
    public int Id { get; }
    public int ProductId { get; }

    public Guid AggregateId { get; }
    public string Url { get; }

    public ProductPictureUpdatedEvent(int id, int productId, Guid aggregateId, string url)
    {
        Id = id;
        AggregateId = aggregateId;
        ProductId = productId;
        Url = url;
    }
}