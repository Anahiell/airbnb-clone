using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.CreateFacilityCommand;

[SwaggerSchema("Команда для создания удобства")]
public class CreateFacilityCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Название удобства")]
    public string Name { get; init; } = string.Empty;

    [SwaggerSchema("Имя иконки (например, wifi, pool, kitchen)")]
    public string IconName { get; init; } = string.Empty;
}