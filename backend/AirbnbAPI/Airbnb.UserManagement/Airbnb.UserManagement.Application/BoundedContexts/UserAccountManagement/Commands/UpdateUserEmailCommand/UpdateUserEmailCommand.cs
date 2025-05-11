using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.ChangeUserPassword;

[SwaggerSchema("Команда для обновления email пользователя")]
public class UpdateUserEmailCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID пользователя")]
    public int Id { get; init; }

    [SwaggerSchema("Новый Email")]
    public string Email { get; init; } = string.Empty;
}