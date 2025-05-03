using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands;

[SwaggerSchema("Команда для удаления продукта")]
public class DeleteProductCommand : ICommand<Result>
{
    [SwaggerSchema("ID продукта для удаления")]
    public int Id { get; init; }
}