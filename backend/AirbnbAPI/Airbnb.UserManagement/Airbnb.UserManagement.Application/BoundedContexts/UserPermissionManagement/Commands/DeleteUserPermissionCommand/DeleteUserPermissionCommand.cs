using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.DeleteUserPermissionCommand;

[SwaggerSchema("Команда для удаления пользовательского разрешения")]
public class DeleteUserPermissionCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID пользовательского разрешения")]
    public int Id { get; init; }
    
    [SwaggerSchema("ID разрешения")]
    public int PermissionId { get; init; }
}