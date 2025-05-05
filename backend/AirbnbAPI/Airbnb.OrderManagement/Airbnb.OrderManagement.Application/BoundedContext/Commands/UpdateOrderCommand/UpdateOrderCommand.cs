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

    [SwaggerSchema("Дата начала аренды (формат: MM.dd.yyyy HH:mm)")]
    public string DateStart { get; init; } = default!;

    [SwaggerSchema("Дата окончания аренды (формат: MM.dd.yyyy HH:mm)")]
    public string DateEnd { get; init; } = default!;

    [SwaggerSchema("Часовой пояс, например: Europe/Paris")]
    public string TimeZone { get; init; } = default!;
}