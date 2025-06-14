namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;

public record ProductReviewDeletedEvent
    : IReviewEvent
{
    public int ProductId { get; init; }
    public int Id { get; set; }
}