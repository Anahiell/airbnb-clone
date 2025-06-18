using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Projections.FeatureUpdatedProjection;

public class FeatureUpdatedProjection : INotificationHandler<FeatureUpdatedEvent>
{
    private readonly IProjectionRepository<FeatureEntityInfo> _repository;

    public FeatureUpdatedProjection(IProjectionRepository<FeatureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(FeatureUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updated = new FeatureEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Name,
            Forcibly = @event.Forcibly,
            Price = @event.Price
        };

        await _repository.UpsertAsync(updated);
    }
}