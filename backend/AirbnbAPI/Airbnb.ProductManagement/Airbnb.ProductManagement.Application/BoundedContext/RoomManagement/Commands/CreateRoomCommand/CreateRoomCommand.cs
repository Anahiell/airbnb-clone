using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Commands.CreateRoomCommand;

[SwaggerSchema("Команда для создания комнаты (Room)")]
public class CreateRoomCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Название комнаты")]
    public string Name { get; init; } = string.Empty;

    [SwaggerSchema("Вместимость комнаты")]
    public int Capacity { get; init; }
}