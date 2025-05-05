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

    public DomainReview()
    {
    }

    public DomainReview(string title, string description, int rating, DateTime createdAt, int userId, int productId)
    {
        Title = title;
        Description = description;
        Rating = rating;
        CreatedAt = createdAt;
        UserId = userId;
        ProductId = productId;

        RaiseEvent(new ReviewCreatedEvent(Id, title, description, rating, createdAt, userId, productId));
    }

    #region Aggregate Methods

    public void CreateReview(string title, string description, int rating, DateTime createdAt, int userId, int productId)
    {
        Title = title;
        Description = description;
        Rating = rating;
        CreatedAt = createdAt;
        UserId = userId;
        ProductId = productId;

        RaiseEvent(new ReviewCreatedEvent(Id, title, description, rating, createdAt, userId, productId));
    }

    public void UpdateReview(string title, string description, int rating, DateTime createdAt, int userId, int productId)
    {
        Title = title;
        Description = description;
        Rating = rating;
        CreatedAt = createdAt; ;

        RaiseEvent(new ReviewUpdatedEvent(Id, title, description, rating, createdAt, userId, productId));
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
    }

    private void OnReviewDeletedEvent(ReviewDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion
}