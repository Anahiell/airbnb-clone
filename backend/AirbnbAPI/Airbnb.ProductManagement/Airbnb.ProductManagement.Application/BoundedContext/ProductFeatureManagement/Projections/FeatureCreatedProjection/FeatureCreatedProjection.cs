using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Projections.FeatureCreatedProjection;

public class FeatureCreatedProjection : INotificationHandler<FeatureCreatedEvent>
{
    private readonly IProjectionRepository<FeatureEntityInfo> _repository;

    public FeatureCreatedProjection(IProjectionRepository<FeatureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(FeatureCreatedEvent @event, CancellationToken cancellationToken)
    {
        var feature = new FeatureEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Name,
            Forcibly = @event.Forcibly,
            Price = @event.Price
        };

        await _repository.UpsertAsync(feature);
    }
}