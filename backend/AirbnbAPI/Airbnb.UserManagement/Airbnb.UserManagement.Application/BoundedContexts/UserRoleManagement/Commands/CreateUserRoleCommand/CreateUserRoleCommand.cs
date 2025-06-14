using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.CreateUserRoleCommand;

[SwaggerSchema("Команда для создания связи пользователь-роль")]
public class CreateUserRoleCommand : ICommand<Result<int>>
{
    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }

    [SwaggerSchema("ID роли")]
    public int RoleId { get; init; }
}