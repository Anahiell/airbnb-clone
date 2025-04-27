using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.OrderManagement.Application.BoundedContext.Commands.UpdateOrderCommand;

[SwaggerSchema("Команда для обновления заказа")]
public class UpdateOrderCommand : ICommand<Result>
{
    [SwaggerSchema("ID заказа для обновления")]
    public int Id { get; init; }

    [SwaggerSchema("ID продукта")]
    public int ProductId { get; init; }

    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }

    [SwaggerSchema("Дата начала аренды")]
    public DateTime DateStart { get; init; }

    [SwaggerSchema("Дата окончания аренды")]
    public DateTime DateEnd { get; init; }
}