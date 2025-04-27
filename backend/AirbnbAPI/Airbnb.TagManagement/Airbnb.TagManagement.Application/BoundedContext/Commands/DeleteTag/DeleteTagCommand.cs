using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.TagsManagement.Application.BoundedContext.Commands.DeleteTag;

[SwaggerSchema("Команда для удаления тега")]
public class DeleteTagCommand : ICommand<Result>
{
    [SwaggerSchema("ID тега для удаления")]
    public int Id { get; init; }
}