using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Commands.CreateDocumentCommand;

[SwaggerSchema("Команда создания документа")]
public class CreateDocumentCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Идентификатор пользователя")]
    [FromForm]
    public required int UserId { get; init; }

    [SwaggerSchema("Тип документа (Passport, DriverLicense)")]
    [FromForm]
    public required DocumentTypeEnum DocumentType { get; init; }

    [SwaggerSchema("Файл документа")]
    [FromForm]
    public required IFormFile File { get; init; }
}