using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.UpdateUserRoleCommand;
[SwaggerSchema("Команда для обновления связи пользователь-роль")]
public class UpdateUserRoleCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }

    [SwaggerSchema("Старый ID роли")]
    public int OldRoleId { get; init; }

    [SwaggerSchema("Новый ID роли")]
    public int NewRoleId { get; init; }
}