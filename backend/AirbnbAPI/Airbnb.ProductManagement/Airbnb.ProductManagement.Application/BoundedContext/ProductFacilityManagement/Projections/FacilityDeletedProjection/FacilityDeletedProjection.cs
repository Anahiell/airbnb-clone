using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Projections.ProductFacilityDeletedProjection;

public class FacilityDeletedProjection : INotificationHandler<FacilityDeletedEvent>
{
    private readonly IProjectionRepository<FacilityEntityInfo> _repository;

    public FacilityDeletedProjection(IProjectionRepository<FacilityEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(FacilityDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}