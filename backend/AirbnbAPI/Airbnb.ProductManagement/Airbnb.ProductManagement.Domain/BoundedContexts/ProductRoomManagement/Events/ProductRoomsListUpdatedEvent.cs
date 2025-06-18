using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;

public class ProductRoomsListUpdatedEvent : DomainEvent
{
    public int ProductId { get; init; }
    public List<(int Id, string Name)> PictureData { get; init; }

    public ProductRoomsListUpdatedEvent(int productId, List<(int Id, string Name)> pictureData)
    {
        ProductId = productId;
        PictureData = pictureData;
    }
}