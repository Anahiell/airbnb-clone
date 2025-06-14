using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.CreateLanguageCommand;

[SwaggerSchema("Команда для создания языка")]
public class CreateLanguageCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Название языка")]
    public string Name { get; init; } = string.Empty;
}