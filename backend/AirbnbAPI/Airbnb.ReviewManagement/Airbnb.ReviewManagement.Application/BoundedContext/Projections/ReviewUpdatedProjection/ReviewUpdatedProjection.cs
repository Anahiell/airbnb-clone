using Airbnb.MongoRepository.Interfaces;
using Airbnb.ReviewManagement.Application.BoundedContext.QueryObjects;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Events;
using MediatR;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Projections.ReviewUpdatedProjection;

public class ReviewUpdatedProjection : INotificationHandler<ReviewUpdatedEvent>
{
    private readonly IProjectionRepository<ReviewEntityInfo> _repository;

    public ReviewUpdatedProjection(IProjectionRepository<ReviewEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(ReviewUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updatedReview = new ReviewEntityInfo
        {
            Id = @event.AggregateId,
            Description = @event.Description,
            Rating = @event.Rating,
            UserId = @event.UserId
        };

        await _repository.UpdateAsync(updatedReview);
    }
}