using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Projections.RoomCreatedProjection;

public class RoomCreatedProjection : INotificationHandler<RoomCreatedEvent>
{
    private readonly IProjectionRepository<RoomEntityInfo> _repository;

    public RoomCreatedProjection(IProjectionRepository<RoomEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(RoomCreatedEvent @event, CancellationToken cancellationToken)
    {
        var room = new RoomEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Name,
            Capacity = @event.Capacity
        };

        await _repository.UpsertAsync(room);
    }
}