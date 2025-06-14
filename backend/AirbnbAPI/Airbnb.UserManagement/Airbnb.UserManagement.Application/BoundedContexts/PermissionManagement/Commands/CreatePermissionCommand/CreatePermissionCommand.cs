using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.CreatePermissionCommand;

[SwaggerSchema("Команда для создания разрешения")]
public class CreatePermissionCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Название разрешения")]
    public string Name { get; init; } = string.Empty;
}