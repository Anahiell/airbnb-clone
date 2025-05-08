using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.CreateProductTagCommand;

[SwaggerSchema("Команда для создания связи между продуктом и тегом")]
public class CreateProductTagCommand : ICommand<Result<int>>
{
    [SwaggerSchema("ID продукта")]
    public int ProductId { get; init; }

    [SwaggerSchema("ID тега")]
    public int TagId { get; init; }
}