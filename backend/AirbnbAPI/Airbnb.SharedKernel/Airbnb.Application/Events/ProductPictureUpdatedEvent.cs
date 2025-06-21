namespace Airbnb.ProductManagement.Application.BoundedContext.Events;

public class ProductPictureUpdatedEvent
{
    public int ProductId { get; set; }
    public byte[] PictureData { get; set; }
    public int? RoomId { get; set; }
    
    public string? RoomName { get; set; }
}