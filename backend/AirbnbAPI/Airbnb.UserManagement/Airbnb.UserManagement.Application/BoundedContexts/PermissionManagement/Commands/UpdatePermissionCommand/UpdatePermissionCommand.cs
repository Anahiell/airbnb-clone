using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.UpdatePermissionCommand;

[SwaggerSchema("Команда для обновления разрешения")]
public class UpdatePermissionCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID разрешения")]
    public int Id { get; init; }

    [SwaggerSchema("Новое название разрешения")]
    public string Name { get; init; } = string.Empty;
}