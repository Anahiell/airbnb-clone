using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Commands.DeleteDocumentCommand;

[SwaggerSchema("Команда удаления документа")]
public class DeleteDocumentCommand : ICommand<Result>
{
    [SwaggerSchema("Идентификатор документа")]
    public required int DocumentId { get; init; }
}