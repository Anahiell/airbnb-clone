namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductOrder;

public record ProductOrderUpdatedEvent(int OrderId, int ProductId, int UserId, DateTime DateStart, DateTime DateEnd)
    : IOrderEvent;