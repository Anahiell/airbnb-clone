using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Commands.UpdateRoomCommand;

[SwaggerSchema("Команда для обновления комнаты")]
public class UpdateRoomCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID комнаты")]
    public int Id { get; init; }

    [SwaggerSchema("Новое название комнаты")]
    public string Name { get; init; } = string.Empty;

    [SwaggerSchema("Новая вместимость комнаты")]
    public int Capacity { get; init; }
}