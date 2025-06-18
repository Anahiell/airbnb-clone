using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Commands.DeleteRoomCommand;

[SwaggerSchema("Команда для удаления комнаты")]
public class DeleteRoomCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID комнаты")]
    public int Id { get; init; }
}