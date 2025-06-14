namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture;

public record ProductPictureUpdatedEvent(
    int ProductId,
    int PictureId,
    string Url,
    bool IsArchived,
    DateTime UpdatedAt
) : IPictureEvent;