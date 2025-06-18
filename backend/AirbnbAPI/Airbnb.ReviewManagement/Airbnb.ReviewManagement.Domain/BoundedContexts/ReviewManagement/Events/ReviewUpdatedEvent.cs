using Airbnb.SharedKernel;

namespace Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Events;

public class ReviewUpdatedEvent : DomainEvent
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public int Rating { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int UserId { get; private set; }
    public int ProductId { get; private set; }
    
    public int? Cleanliness { get; private set; }
    public int? Communication { get; private set; }
    public int? Arrival { get; private set; }
    public int? Accuracy { get; private set; }
    public int? Location { get; private set; }
    public int? PriceToQuality { get; private set; }


    public ReviewUpdatedEvent(int aggregateId, string title, string description, int rating,
        DateTime createdAt, int userId, int productId,
        int? cleanliness = null, int? communication = null, int? arrival = null, int? accuracy = null,
        int? location = null, int? priceToQuality = null)
        : base(aggregateId)
    {
        Title = title;
        Description = description;
        Rating = rating;
        CreatedAt = createdAt;
        UserId = userId;
        ProductId = productId;
        
        Cleanliness = cleanliness;
        Communication = communication;
        Arrival = arrival;
        Accuracy = accuracy;
        Location = location;
        PriceToQuality = priceToQuality;
    }
}