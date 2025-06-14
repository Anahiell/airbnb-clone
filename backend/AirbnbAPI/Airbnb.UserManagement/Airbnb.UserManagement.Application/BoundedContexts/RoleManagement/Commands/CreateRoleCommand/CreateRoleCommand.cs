using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.CreateRoleCommand;

[SwaggerSchema("Команда для создания роли")]
public class CreateRoleCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Название роли")]
    public string Name { get; init; } = string.Empty;
}