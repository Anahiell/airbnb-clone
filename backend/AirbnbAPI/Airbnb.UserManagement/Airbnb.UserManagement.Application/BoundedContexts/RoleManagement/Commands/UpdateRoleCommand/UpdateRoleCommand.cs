using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.UpdateRoleCommand;

[SwaggerSchema("Команда для обновления роли")]
public class UpdateRoleCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID роли")]
    public int Id { get; init; }

    [SwaggerSchema("Новое название роли")]
    public string Name { get; init; } = string.Empty;
}