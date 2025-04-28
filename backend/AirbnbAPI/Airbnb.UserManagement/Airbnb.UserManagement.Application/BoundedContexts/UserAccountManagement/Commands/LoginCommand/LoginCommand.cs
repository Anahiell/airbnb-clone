using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.LoginCommand;

[SwaggerSchema("Команда для логина пользователя")]
public class LoginCommand : ICommand<Result<string>>
{
    [SwaggerSchema("Email пользователя")]
    public string Email { get; init; } = string.Empty;

    [SwaggerSchema("Пароль пользователя")]
    public string Password { get; init; } = string.Empty;
}