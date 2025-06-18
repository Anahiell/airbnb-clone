using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.DeleteFeatureCommand;

[SwaggerSchema("Команда для удаления характеристики")]
public class DeleteFeatureCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID характеристики")]
    public int Id { get; init; }
}