namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture;

public class ProductPictureCreatedEvent : IPictureEvent
{
    public int ProductId { get; set; }
    public int PictureId { get; set; }
    public string Url { get; set; } = default!;
    public bool IsArchived { get; set; }
    public DateTime UpdatedAt { get; set; }
}