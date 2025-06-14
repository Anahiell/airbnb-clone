namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture;

public class ProductPictureDeletedEvent : IPictureEvent
{
    public int ProductId { get; set; }
    public int PictureId { get; set; }
}