namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;

public interface ITagEvent : IProductEvent
{
    int ProductId { get; }
    int TagId { get; }
}