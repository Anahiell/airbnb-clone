using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.DeleteProductTagCommand;

[SwaggerSchema("Команда для удаления связи между продуктом и тегом")]
public class DeleteProductTagCommand : ICommand<Result>
{
    [SwaggerSchema("ID связи")]
    public int Id { get; init; }
}