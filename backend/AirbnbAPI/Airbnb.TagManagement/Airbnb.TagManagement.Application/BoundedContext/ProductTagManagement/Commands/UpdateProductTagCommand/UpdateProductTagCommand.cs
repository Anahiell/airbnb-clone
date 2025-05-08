using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.UpdateProductTagCommand;

[SwaggerSchema("Команда для обновления связи между продуктом и тегом")]
public class UpdateProductTagCommand : ICommand<Result>
{
    [SwaggerSchema("ID связи")]
    public int Id { get; init; }

    [SwaggerSchema("Новый ID продукта")]
    public int NewProductId { get; init; }

    [SwaggerSchema("Новый ID тега")]
    public int NewTagId { get; init; }
}