using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Commands.UpdateDocumentCommand;

[SwaggerSchema("Команда обновления документа")]
public class UpdateDocumentCommand : ICommand<Result>
{
    [SwaggerSchema("Идентификатор документа")]
    public required int DocumentId { get; init; }

    [SwaggerSchema("Обновленные данные документа")]
    public required Dictionary<string, object> Data { get; init; }
}