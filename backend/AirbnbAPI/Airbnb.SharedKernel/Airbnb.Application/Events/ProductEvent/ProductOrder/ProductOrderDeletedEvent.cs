namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductOrder;

public record ProductOrderDeletedEvent(int ProductId, int OrderId)
    : IOrderEvent;
