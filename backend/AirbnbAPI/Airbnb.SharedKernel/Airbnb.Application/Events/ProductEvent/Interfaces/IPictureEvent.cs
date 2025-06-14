namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;

public interface IPictureEvent : IProductEvent 
{
    int PictureId { get; }
    int ProductId { get; }
}