using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands;

[SwaggerSchema("Команда для обновления продукта")]
public class UpdateProductCommand : ICommand<Result>
{
    [SwaggerSchema("ID продукта для обновления")]
    public int Id { get; init; }

    [SwaggerSchema("Название продукта")]
    public string ProductTitle { get; init; } = string.Empty;

    [SwaggerSchema("Описание продукта")]
    public string ProductDescription { get; init; } = string.Empty;

    [SwaggerSchema("Цена продукта")]
    public int ProductPrice { get; init; }

    [SwaggerSchema("Рейтинг продукта")]
    public int ProductRating { get; init; }

    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }

    [SwaggerSchema("ID типа апартаментов")]
    public int ApartmentTypeId { get; init; }

    [SwaggerSchema("ID юридического адреса")]
    public int AddressLegalId { get; init; }
}