using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Projections.RoomDeletedProjection;

public class RoomDeletedProjection : INotificationHandler<RoomDeletedEvent>
{
    private readonly IProjectionRepository<RoomEntityInfo> _repository;

    public RoomDeletedProjection(IProjectionRepository<RoomEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(RoomDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}