using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;

public interface IProductEvent : INotification
{
    int ProductId { get; }
}