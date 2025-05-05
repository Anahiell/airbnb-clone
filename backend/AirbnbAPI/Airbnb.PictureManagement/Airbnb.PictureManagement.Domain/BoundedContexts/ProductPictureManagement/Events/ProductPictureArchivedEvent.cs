using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;

public class ProductPictureArchivedEvent : DomainEvent
{
    public int Id { get; }

    public ProductPictureArchivedEvent(int id)
    {
        Id = id;
    }
}