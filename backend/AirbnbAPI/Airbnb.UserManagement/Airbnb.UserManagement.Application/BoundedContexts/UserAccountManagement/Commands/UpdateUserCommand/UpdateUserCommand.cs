using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserCommand;

[SwaggerSchema("Команда для обновления пользователя")]
public class UpdateUserCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID пользователя для обновления")]
    public int Id { get; init; }

    [SwaggerSchema("Полное имя пользователя")]
    public string FullName { get; init; } = string.Empty;

    [SwaggerSchema("Email пользователя")]
    public string Email { get; init; } = string.Empty;

    [SwaggerSchema("Роль пользователя")]
    public List<UserRole> Roles { get; init; } = new();

    [SwaggerSchema("Дата рождения пользователя")]
    public DateTime DateOfBirth { get; init; }
}