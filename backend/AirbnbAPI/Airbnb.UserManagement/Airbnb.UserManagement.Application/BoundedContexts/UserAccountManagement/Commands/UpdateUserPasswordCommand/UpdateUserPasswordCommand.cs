using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserPasswordCommand;

[SwaggerSchema("Команда для обновления пароля пользователя")]
public class UpdateUserPasswordCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID пользователя")]
    public int Id { get; init; }

    [SwaggerSchema("Текущий пароль")]
    public string CurrentPassword { get; init; } = string.Empty;

    [SwaggerSchema("Новый пароль")]
    public string NewPassword { get; init; } = string.Empty;
}