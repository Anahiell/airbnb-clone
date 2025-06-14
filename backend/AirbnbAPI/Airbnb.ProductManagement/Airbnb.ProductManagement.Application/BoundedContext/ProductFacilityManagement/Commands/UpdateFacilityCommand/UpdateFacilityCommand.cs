using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.UpdateFacilityCommand;

[SwaggerSchema("Команда для обновления удобства")]
public class UpdateFacilityCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID удобства")]
    public int Id { get; init; }

    [SwaggerSchema("Новое название удобства")]
    public string Name { get; init; } = string.Empty;

    [SwaggerSchema("Новое имя иконки")]
    public string IconName { get; init; } = string.Empty;
}