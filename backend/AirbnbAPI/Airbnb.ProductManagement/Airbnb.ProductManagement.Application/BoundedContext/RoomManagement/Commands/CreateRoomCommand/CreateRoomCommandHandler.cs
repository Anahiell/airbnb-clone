using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;
using Airbnb.Domain.BoundedContexts.RoomManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.RoomManagement.Interfaces;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Commands.CreateRoomCommand;

public class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand, Result<int>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMediator _mediator;

    public CreateRoomCommandHandler(IRoomRepository roomRepository, IMediator mediator)
    {
        _roomRepository = roomRepository;
        _mediator = mediator;
    }

    public async Task<Result<int>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = new Room(request.Name, request.Capacity);
        var result = await _roomRepository.AddAsync(room, cancellationToken);

        await _mediator.Publish(new RoomCreatedEvent(room.Id, room.Name, room.Capacity), cancellationToken);

        return Result<int>.Success(result);
    }
}