using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.DeletePermissionCommand;

[SwaggerSchema("Команда для удаления разрешения")]
public class DeletePermissionCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID разрешения")]
    public int Id { get; init; }
}