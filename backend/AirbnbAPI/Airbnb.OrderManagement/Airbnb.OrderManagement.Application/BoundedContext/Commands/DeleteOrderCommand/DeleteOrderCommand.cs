using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.OrderManagement.Application.BoundedContext.Commands.DeleteOrderCommand;

[SwaggerSchema("Команда для удаления заказа")]
public class DeleteOrderCommand : ICommand<Result>
{
    [SwaggerSchema("ID заказа для удаления")]
    public int Id { get; init; }
}