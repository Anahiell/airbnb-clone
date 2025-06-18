using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Events;
using Airbnb.SharedKernel;

namespace Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Aggregates;

public class DomainReview : AggregateRoot
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

    public DomainReview()
    {
    }

    public DomainReview(string title, string description, int rating, DateTime createdAt, int userId, int productId,
        int? cleanliness = null, int? communication = null, int? arrival = null, int? accuracy = null,
        int? location = null, int? priceToQuality = null)
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

        RaiseEvent(new ReviewCreatedEvent(Id, title, description, rating, createdAt, userId, productId));
    }

    #region Aggregate Methods

    public void CreateReview(string title, string description, int rating, DateTime createdAt, int userId, int productId,
        int? cleanliness = null, int? communication = null, int? arrival = null, int? accuracy = null,
        int? location = null, int? priceToQuality = null)
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

        RaiseEvent(new ReviewCreatedEvent(Id, title, description, rating, createdAt, userId, productId,
            cleanliness, communication, arrival, accuracy, location, priceToQuality));
    }

    public void UpdateReview(string title, string description, int rating, DateTime createdAt, int userId, int productId,
        int? cleanliness = null, int? communication = null, int? arrival = null, int? accuracy = null,
        int? location = null, int? priceToQuality = null)
    {
        Title = title;
        Description = description;
        Rating = rating;
        CreatedAt = createdAt;
        
        Cleanliness = cleanliness;
        Communication = communication;
        Arrival = arrival;
        Accuracy = accuracy;
        Location = location;
        PriceToQuality = priceToQuality;

        RaiseEvent(new ReviewUpdatedEvent(Id, title, description, rating, createdAt, userId, productId,
            cleanliness, communication, arrival, accuracy, location, priceToQuality));
    }

    public void DeleteReview()
    {
        RaiseEvent(new ReviewDeletedEvent(Id));
    }

    #endregion

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case ReviewCreatedEvent e:
                OnReviewCreatedEvent(e);
                break;
            case ReviewUpdatedEvent e:
                OnReviewUpdatedEvent(e);
                break;
            case ReviewDeletedEvent e:
                OnReviewDeletedEvent(e);
                break;
        }
    }

    private void OnReviewCreatedEvent(ReviewCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Title = @event.Title;
        Description = @event.Description;
        Rating = @event.Rating;
        CreatedAt = @event.CreatedAt;
        UserId = @event.UserId;
        ProductId = @event.ProductId;
        
        Cleanliness = @event.Cleanliness;
        Communication = @event.Communication;
        Arrival = @event.Arrival;
        Accuracy = @event.Accuracy;
        Location = @event.Location;
        PriceToQuality = @event.PriceToQuality;
    }

    private void OnReviewUpdatedEvent(ReviewUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        Title = @event.Title;
        Description = @event.Description;
        Rating = @event.Rating;
        CreatedAt = @event.CreatedAt;
        UserId = @event.UserId;
        ProductId = @event.ProductId;
        
        Cleanliness = @event.Cleanliness;
        Communication = @event.Communication;
        Arrival = @event.Arrival;
        Accuracy = @event.Accuracy;
        Location = @event.Location;
        PriceToQuality = @event.PriceToQuality;
    }

    private void OnReviewDeletedEvent(ReviewDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion
}