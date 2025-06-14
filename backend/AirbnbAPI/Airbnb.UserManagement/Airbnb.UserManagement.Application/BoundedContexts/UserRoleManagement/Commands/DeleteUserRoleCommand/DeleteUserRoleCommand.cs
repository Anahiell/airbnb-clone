using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.DeleteUserRoleCommand;

[SwaggerSchema("Команда для удаления связи пользователь-роль")]
public class DeleteUserRoleCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }

    [SwaggerSchema("ID роли")]
    public int RoleId { get; init; }
}