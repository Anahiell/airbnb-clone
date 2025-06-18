using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.UpdateFeatureCommand;


[SwaggerSchema("Команда для обновления характеристики")]
public class UpdateFeatureCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID характеристики")]
    public int Id { get; init; }

    [SwaggerSchema("Новое название характеристики")]
    public string Name { get; init; } = string.Empty;

    [SwaggerSchema("Обязательна ли характеристика")]
    public bool Forcibly { get; init; }

    [SwaggerSchema("Новая цена характеристики")]
    public int Price { get; init; }
}