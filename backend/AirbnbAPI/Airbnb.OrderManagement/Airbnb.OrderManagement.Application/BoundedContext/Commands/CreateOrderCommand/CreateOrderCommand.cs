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

    [SwaggerSchema("Дата начала аренды (формат: MM.dd.yyyy HH:mm)")]
    public string DateStart { get; init; } = default!;

    [SwaggerSchema("Дата окончания аренды (формат: MM.dd.yyyy HH:mm)")]
    public string DateEnd { get; init; } = default!;

    [SwaggerSchema("Часовой пояс, например: Europe/Paris")]
    public string TimeZone { get; init; } = default!;
}