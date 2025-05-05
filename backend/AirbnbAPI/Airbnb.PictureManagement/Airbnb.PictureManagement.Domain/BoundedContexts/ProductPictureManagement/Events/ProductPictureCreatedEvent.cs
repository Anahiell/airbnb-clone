using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;

public class ProductPictureCreatedEvent : DomainEvent
{
    public int Id { get; }
    public Guid AggregateId { get; }
    public string Url { get; }
    public int ProductId { get; }
    public DateTime CreatedDate { get; }

    public ProductPictureCreatedEvent(int id, Guid aggregateId, string url, int productId, DateTime createdDate)
    {
        Id = id;
        AggregateId = aggregateId;
        Url = url;
        ProductId = productId;
        CreatedDate = createdDate;
    }
}