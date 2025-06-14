using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Projections.ProductFacilityCreatedProjection;

public class FacilityCreatedProjection : INotificationHandler<FacilityCreatedEvent>
{
    private readonly IProjectionRepository<FacilityEntityInfo> _repository;

    public FacilityCreatedProjection(IProjectionRepository<FacilityEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(FacilityCreatedEvent @event, CancellationToken cancellationToken)
    {
        var facility = new FacilityEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Name,
            IconName = @event.IconName
        };

        await _repository.UpsertAsync(facility);
    }
}