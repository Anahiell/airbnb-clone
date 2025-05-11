namespace Airbnb.ProductManagement.Application.BoundedContext.Events;


public class ReviewUpdatedEvent
{
    public int ReviewId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
}