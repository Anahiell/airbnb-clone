namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductOrder;

public record ProductOrderCreatedEvent(int ProductId, int OrderId, DateTime DateStart, DateTime DateEnd)
    : IOrderEvent;