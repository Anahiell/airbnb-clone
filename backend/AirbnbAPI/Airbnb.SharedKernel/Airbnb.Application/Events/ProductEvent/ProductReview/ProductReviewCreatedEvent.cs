namespace Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;

public record ProductReviewCreatedEvent() : IReviewEvent
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}