namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;

public interface IReviewEvent : IProductEvent 
{
    int Id { get; }
}