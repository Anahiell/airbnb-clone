using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Commands;

[SwaggerSchema("Команда для создания отзыва")]
public class CreateReviewCommand : ICommand<Result<int>>
{
    [SwaggerSchema("Текст отзыва")]
    public string Comment { get; init; } = string.Empty;

    [SwaggerSchema("Описание отзыва")]
    public string Description { get; init; } = string.Empty;

    [SwaggerSchema("Оценка отзыва (1-5)")]
    public int Rating { get; init; }
    
    [SwaggerSchema("Идентификатор продукта")]
    public int ProductId { get; init; }
    
    [SwaggerSchema("Идентификатор пользователя")]
    public int UserId { get; init; }
}