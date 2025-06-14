using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.DeleteFacilityCommand;

[SwaggerSchema("Команда для удаления удобства")]
public class DeleteFacilityCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID удобства")]
    public int Id { get; init; }
}