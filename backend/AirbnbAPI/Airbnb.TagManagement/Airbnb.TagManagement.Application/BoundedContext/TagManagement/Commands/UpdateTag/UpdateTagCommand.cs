using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.TagsManagement.Application.BoundedContext.Commands.UpdateTag;

[SwaggerSchema("Команда для обновления тега")]
public class UpdateTagCommand : ICommand<Result>
{
    [SwaggerSchema("ID тега")]
    public int Id { get; init; }

    [SwaggerSchema("Новое название тега")]
    public string NewName { get; init; } = string.Empty;
}
