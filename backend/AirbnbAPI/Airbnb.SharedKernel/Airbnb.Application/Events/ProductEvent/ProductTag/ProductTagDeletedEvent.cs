namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductTag;

public record ProductTagDeletedEvent(int ProductId, int TagId) : ITagEvent;
