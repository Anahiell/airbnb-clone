using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.DeleteRoleCommand;

[SwaggerSchema("Команда для удаления роли")]
public class DeleteRoleCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID роли")]
    public int Id { get; init; }
}