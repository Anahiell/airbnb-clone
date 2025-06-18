using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.CreateFeatureCommand;

[SwaggerSchema("Команда для создания характеристики (Feature)")]
public class CreateFeatureCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Название характеристики")]
    public string Name { get; init; } = string.Empty;

    [SwaggerSchema("Обязательна ли характеристика")]
    public bool Forcibly { get; init; }

    [SwaggerSchema("Цена характеристики")]
    public int Price { get; init; }
}