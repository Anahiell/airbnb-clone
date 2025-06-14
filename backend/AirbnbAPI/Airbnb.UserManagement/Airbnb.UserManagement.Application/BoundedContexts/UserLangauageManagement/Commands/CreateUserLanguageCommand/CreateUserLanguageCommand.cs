using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.CreateUserLanguageCommand;

[SwaggerSchema("Команда для добавления языка пользователю")]
public class CreateUserLanguageCommand : ICommand<Result<int>>
{
    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }

    [SwaggerSchema("ID языка")]
    public int LanguageId { get; init; }
}