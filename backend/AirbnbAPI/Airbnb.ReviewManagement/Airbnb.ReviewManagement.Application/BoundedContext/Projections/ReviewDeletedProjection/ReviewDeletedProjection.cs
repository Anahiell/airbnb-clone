using Airbnb.MongoRepository.Interfaces;
using Airbnb.ReviewManagement.Application.BoundedContext.QueryObjects;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Events;
using MediatR;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Projections.ReviewDeletedProjection;

public class ReviewDeletedProjection : INotificationHandler<ReviewDeletedEvent>
{
    private readonly IProjectionRepository<ReviewEntityInfo> _repository;

    public ReviewDeletedProjection(IProjectionRepository<ReviewEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(ReviewDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}