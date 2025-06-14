namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductTag;

public record ProductTagCreatedEvent(int ProductId, int TagId, string TagName) : ITagEvent;