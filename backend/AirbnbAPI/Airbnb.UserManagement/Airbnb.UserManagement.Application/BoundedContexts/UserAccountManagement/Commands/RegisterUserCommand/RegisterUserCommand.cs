using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.RegisterUserCommand;

[SwaggerSchema("Команда для регистрации нового пользователя")]
public class RegisterUserCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Полное имя пользователя")]
    public string FullName { get; init; } = string.Empty;

    [SwaggerSchema("Email пользователя")]
    public string Email { get; init; } = string.Empty;

    [SwaggerSchema("Пароль пользователя")]
    public string Password { get; init; } = string.Empty;

    [SwaggerSchema("Дата рождения пользователя")]
    public DateTime DateOfBirth { get; init; }

    [SwaggerSchema("Роли пользователя")]
    public List<UserRole> Roles { get; init; } = new List<UserRole>();
    
    [SwaggerSchema("Фотография пользователя")]
    public IFormFile UserPicture { get; init; }
}