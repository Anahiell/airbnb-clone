namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductTag;

public record ProductTagUpdatedEvent(int ProductId, int TagId, string TagName) : ITagEvent;
