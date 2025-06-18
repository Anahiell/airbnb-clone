using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Projections.FeatureDeletedProjection;

public class FeatureDeletedProjection : INotificationHandler<FeatureDeletedEvent>
{
    private readonly IProjectionRepository<FeatureEntityInfo> _repository;

    public FeatureDeletedProjection(IProjectionRepository<FeatureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(FeatureDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}