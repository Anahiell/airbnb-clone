using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.DeleteLanguageCommand;

[SwaggerSchema("Команда для удаления языка")]
public class DeleteLanguageCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID языка")]
    public int Id { get; init; }
}