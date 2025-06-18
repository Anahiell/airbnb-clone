namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture;

public record ProductPictureUpdatedEvent(
    int ProductId,
    int PictureId,
    string Url,
    bool IsArchived,
    DateTime UpdatedAt,
    int? RoomId = null,
    string? RoomName = null
) : IPictureEvent;