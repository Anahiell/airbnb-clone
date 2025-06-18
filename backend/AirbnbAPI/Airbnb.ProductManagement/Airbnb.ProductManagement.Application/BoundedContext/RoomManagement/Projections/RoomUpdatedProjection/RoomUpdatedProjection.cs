using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Projections.RoomUpdatedProjection;

public class RoomUpdatedProjection : INotificationHandler<RoomUpdatedEvent>
{
    private readonly IProjectionRepository<RoomEntityInfo> _repository;

    public RoomUpdatedProjection(IProjectionRepository<RoomEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(RoomUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updated = new RoomEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Name,
            Capacity = @event.Capacity
        };

        await _repository.UpsertAsync(updated);
    }
}