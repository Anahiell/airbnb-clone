using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UserCreateCommand;

[SwaggerSchema("Команда для создания пользователя")]
public class CreateUserCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Полное имя пользователя")]
    public string FullName { get; init; } = string.Empty;

    [SwaggerSchema("Email пользователя")]
    public string Email { get; init; } = string.Empty;

    [SwaggerSchema("Роль пользователя")]
    public List<UserRole> Roles { get; init; } = new();

    [SwaggerSchema("Дата рождения пользователя")]
    public DateTime DateOfBirth { get; init; }
    
    [SwaggerSchema("Фотография пользователя")]
    public IFormFile UserPicture { get; init; }
}