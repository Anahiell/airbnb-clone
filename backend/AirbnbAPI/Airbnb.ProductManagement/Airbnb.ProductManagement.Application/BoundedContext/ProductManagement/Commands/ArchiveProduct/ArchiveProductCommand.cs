using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.ArchiveProduct;

[SwaggerSchema("Команда для архивирования продукта по ID")]
public class ArchiveProductCommand : ICommand<Result<bool>>
{
    [SwaggerSchema("ID продукта для архивирования")]
    public int ProductId { get; init; }
}