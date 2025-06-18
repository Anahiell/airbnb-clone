using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;
using Airbnb.Domain.BoundedContexts.RoomManagement.Interfaces;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Commands.UpdateRoomCommand;

public class UpdateRoomCommandHandler : ICommandHandler<UpdateRoomCommand, Result<string>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMediator _mediator;

    public UpdateRoomCommandHandler(IRoomRepository roomRepository, IMediator mediator)
    {
        _roomRepository = roomRepository;
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.Id, cancellationToken);
        if (room == null)
            return Result<string>.Failure("Комната не найдена");

        room.Update(request.Name, request.Capacity);

        await _roomRepository.UpdateAsync(room, cancellationToken);
        await _mediator.Publish(new RoomUpdatedEvent(room.Id, room.Name, room.Capacity), cancellationToken);

        return Result<string>.Success("Комната успешно обновлена");
    }
}