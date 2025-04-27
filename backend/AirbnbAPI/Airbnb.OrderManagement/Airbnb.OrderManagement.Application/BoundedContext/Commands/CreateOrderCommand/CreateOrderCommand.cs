using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.OrderManagement.Application.BoundedContext.Commands.CreateOrderCommand;

[SwaggerSchema("Команда для создания заказа")]
public class CreateOrderCommand : ICommand<Result<int>>
{
    [SwaggerSchema("ID продукта")]
    public int ProductId { get; init; }

    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }

    [SwaggerSchema("Дата начала аренды")]
    public DateTime DateStart { get; init; }

    [SwaggerSchema("Дата окончания аренды")]
    public DateTime DateEnd { get; init; }
}