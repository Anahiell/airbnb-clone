using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.UpdateUserPermissionCommand;

[SwaggerSchema("Команда для обновления пользовательского разрешения")]
public class UpdateUserPermissionCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID пользователя")]
    public int Id { get; init; }
    
    [SwaggerSchema("Старый ID разрешения")]
    public int OldPermissionId { get; init; }

    [SwaggerSchema("Новый ID разрешения")]
    public int PermissionId { get; init; }
}