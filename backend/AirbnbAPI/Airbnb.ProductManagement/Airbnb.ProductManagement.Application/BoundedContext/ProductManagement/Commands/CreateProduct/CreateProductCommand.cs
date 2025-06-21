using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.ValueObjects;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.ValueObjects;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;

[SwaggerSchema("Команда для создания продукта")]
public class CreateProductCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Название продукта")]
    public string ProductTitle { get; init; } = string.Empty;

    [SwaggerSchema("Описание продукта")]
    public string ProductDescription { get; init; } = string.Empty;
    
    [SwaggerSchema("Краткое описание продукта")]
    public string ShortDescription { get; init; } = string.Empty;
    
    [SwaggerSchema("Описание локации продукта")]
    public string ProductLocationDescription { get; init; } = string.Empty;

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
    
    [SwaggerSchema("Координаты ширина")]
    public string? Latitude { get; init; }
    
    [SwaggerSchema("Координаты долгота")]
    public string? Longitude { get; init; }
    
    public List<string> ProductTags { get; init; }
    
    [SwaggerSchema("Правила гостей")]
    public GuestRules? GuestRules { get; init; }

    [SwaggerSchema("Политика отмены бронирования")]
    public CancelPolicyEntityInfo? CancelPolicy { get; init; }

    [SwaggerSchema("Домашние правила")]
    public List<string>? HomeRules { get; init; } = new();

    [SwaggerSchema("Правила безопасности")]
    public List<string>? SafetyRules { get; init; } = new();

    [SwaggerSchema("Преимущества продукта")]
    public List<Advantage>? Advantages { get; init; } = new();

    [SwaggerSchema("Особенности продукта")]
    public List<string>? Features { get; init; }
    
    [SwaggerSchema("Удобства продукта")]
    public List<string>? Facilities { get; init; }
}

public class GuestRules
{
    [SwaggerSchema("Максимальное количество гостей")]
    public int MaxGuestsNumber { get; private set; }
    
    [SwaggerSchema("Наличие животных")]
    public bool PetsAllowed { get; private set; }
    
    [SwaggerSchema("Максимальное количество животных")]
    public int? MaxPetsNumber { get; private set; }
    
    [SwaggerSchema("Дополнительная плата за животных")]
    public int? PetsAddedPrice { get; private set; }
}

public class Advantage
{
    [SwaggerSchema("Заголовок преимущества")]
    public string Title { get; init; } = null!;

    [SwaggerSchema("Описание преимущества")]
    public string Description { get; init; } = null!;
}

public record ProductPicture(string PictureName, IFormFile File);