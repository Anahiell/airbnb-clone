using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;

[SwaggerSchema("Команда для создания продукта")]
public class CreateProductCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Название продукта")]
    public string ProductTitle { get; init; } = string.Empty;

    [SwaggerSchema("Описание продукта")]
    public string ProductDescription { get; init; } = string.Empty;

    [SwaggerSchema("Цена продукта")]
    public int ProductPrice { get; init; }

    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }

    [SwaggerSchema("Тип апартаментов")]
    public PropertyTypeEnum ApartmentType { get; init; }

    [SwaggerSchema("Регион")]
    public string Region { get; init; } = string.Empty;

    [SwaggerSchema("Страна")]
    public string Country { get; init; } = string.Empty;

    [SwaggerSchema("Город")]
    public string City { get; init; } = string.Empty;

    [SwaggerSchema("Район")]
    public string District { get; init; } = string.Empty;

    [SwaggerSchema("Дом")]
    public string House { get; init; } = string.Empty;

    [SwaggerSchema("Корпус")]
    public string? Block { get; init; }

    [SwaggerSchema("Квартира")]
    public string? Flat { get; init; }
}