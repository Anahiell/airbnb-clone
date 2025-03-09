using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;

[SwaggerSchema("Команда для создания продукта")]
public class CreateProductCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Название продукта")]
    public string? ProductTitle { get; init; }

    [SwaggerSchema("Описание продукта")]
    public string? ProductDescription { get; init; }

    [SwaggerSchema("Цена продукта")]
    public int ProductPrice { get; init; }

    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }

    [SwaggerSchema("ID типа апартаментов")]
    public int ApartmentTypeId { get; init; }

    [SwaggerSchema("ID юридического адреса")]
    public int AddressLegalId { get; init; }
}