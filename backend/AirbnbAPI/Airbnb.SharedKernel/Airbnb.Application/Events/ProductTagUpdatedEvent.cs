namespace Airbnb.ProductManagement.Application.BoundedContext.Events;

public class ProductTagUpdatedEvent
{
    public int ProductId { get; set; }
    public List<string> ProductTags { get; set; }
}
