using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.DeleteUserLanguageCommand;


[SwaggerSchema("Команда для удаления языка пользователя")]
public class DeleteUserLanguageCommand : ICommand<Result<int>>
{
    [SwaggerSchema("ID записи языка пользователя")]
    public int Id { get; init; }
    
    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }
}