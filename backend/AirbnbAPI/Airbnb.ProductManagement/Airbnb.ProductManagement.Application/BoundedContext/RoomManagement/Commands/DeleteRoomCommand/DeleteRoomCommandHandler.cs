using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;
using Airbnb.Domain.BoundedContexts.RoomManagement.Interfaces;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Commands.DeleteRoomCommand;


public class DeleteRoomCommandHandler : ICommandHandler<DeleteRoomCommand, Result<string>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMediator _mediator;

    public DeleteRoomCommandHandler(IRoomRepository roomRepository, IMediator mediator)
    {
        _roomRepository = roomRepository;
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.Id, cancellationToken);
        if (room == null)
            return Result<string>.Failure("Комната не найдена");

        room.Delete();

        await _roomRepository.DeleteAsync(room.Id, cancellationToken);
        await _mediator.Publish(new RoomDeletedEvent(room.Id), cancellationToken);

        return Result<string>.Success("Комната успешно удалена");
    }
}