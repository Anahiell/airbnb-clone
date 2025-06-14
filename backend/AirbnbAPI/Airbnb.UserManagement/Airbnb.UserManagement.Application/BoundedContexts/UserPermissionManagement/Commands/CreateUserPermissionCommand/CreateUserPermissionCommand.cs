using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.CreateUserPermissionCommand;

[SwaggerSchema("Команда для создания пользовательского разрешения")]
public class CreateUserPermissionCommand : ICommand<Result<int>>
{
    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }

    [SwaggerSchema("ID разрешения")]
    public int PermissionId { get; init; }
}