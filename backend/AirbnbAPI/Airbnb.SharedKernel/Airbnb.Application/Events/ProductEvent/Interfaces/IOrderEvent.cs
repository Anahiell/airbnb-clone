namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;

public interface IOrderEvent : IProductEvent {
    int OrderId { get; }
}