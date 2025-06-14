using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.UpdateUserLanguageCommand;

[SwaggerSchema("Команда для обновления языка пользователя")]
public class UpdateUserLanguageCommand : ICommand<Result<int>>
{
    [SwaggerSchema("ID записи языка пользователя")]
    public int Id { get; init; }
    
    [SwaggerSchema("Новый язык")]
    public int OldLanguageId { get; init; }

    [SwaggerSchema("Новый язык")]
    public int NewLanguageId { get; init; }
}