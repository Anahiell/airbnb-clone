using Airbnb.MongoRepository.Interfaces;
using Airbnb.ReviewManagement.Application.BoundedContext.QueryObjects;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Events;
using MediatR;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Projections.ReviewCreatedProjection;

public class ReviewCreatedProjection : INotificationHandler<ReviewCreatedEvent>
{
    private readonly IProjectionRepository<ReviewEntityInfo> _repository;

    public ReviewCreatedProjection(IProjectionRepository<ReviewEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(ReviewCreatedEvent @event, CancellationToken cancellationToken)
    {
        var review = new ReviewEntityInfo
        {
            Id = @event.AggregateId,
            Description = @event.Description,
            Rating = @event.Rating,
            CreatedAt = @event.CreatedAt,
            UserId = @event.UserId
        };

        await _repository.InsertAsync(review);
    }
}