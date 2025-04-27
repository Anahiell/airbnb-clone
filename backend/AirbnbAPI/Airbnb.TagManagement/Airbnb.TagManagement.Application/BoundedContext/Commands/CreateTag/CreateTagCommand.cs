using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.TagsManagement.Application.BoundedContext.Commands.CreateTag;

[SwaggerSchema("Команда для создания тега")]
public class CreateTagCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Название тега")]
    public string Name { get; init; } = string.Empty;
}