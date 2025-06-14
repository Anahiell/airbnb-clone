using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.UpdateLanguageCommand;

[SwaggerSchema("Команда для обновления языка")]
public class UpdateLanguageCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID языка")]
    public int Id { get; init; }

    [SwaggerSchema("Новое название языка")]
    public string Name { get; init; } = string.Empty;
}