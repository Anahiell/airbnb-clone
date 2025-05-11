namespace Airbnb.ProductManagement.Application.BoundedContext.Events;

public class UserPictureUpdatedEvent
{
    public int UserId { get; set; }
    public byte[] PictureData { get; set; }
}