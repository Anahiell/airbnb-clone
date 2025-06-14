using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Projections.ProductFacilityUpdatedProjection;

public class FacilityUpdatedProjection : INotificationHandler<FacilityUpdatedEvent>
{
    private readonly IProjectionRepository<FacilityEntityInfo> _repository;

    public FacilityUpdatedProjection(IProjectionRepository<FacilityEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(FacilityUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updated = new FacilityEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Name,
            IconName = @event.IconName
        };

        await _repository.UpsertAsync(updated);
    }
}