using Airbnb.MongoRepository.Entities;

namespace Airbnb.ReviewManagement.Application.BoundedContext.QueryObjects;

public class ReviewEntityInfo : IQueryEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
}