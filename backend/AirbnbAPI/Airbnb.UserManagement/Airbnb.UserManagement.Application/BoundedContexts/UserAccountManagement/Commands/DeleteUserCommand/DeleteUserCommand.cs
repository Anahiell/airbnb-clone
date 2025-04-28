using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.DeleteUserCommand;

[SwaggerSchema("Команда для удаления пользователя")]
public class DeleteUserCommand : ICommand<Result>
{
    [SwaggerSchema("ID пользователя для удаления")]
    public int Id { get; init; }
}