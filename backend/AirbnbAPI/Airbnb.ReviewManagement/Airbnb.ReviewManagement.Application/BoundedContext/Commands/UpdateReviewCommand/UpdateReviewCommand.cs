using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Commands.UpdateReviewCommand;

[SwaggerSchema("Команда для обновления отзыва")]
public class UpdateReviewCommand : ICommand<Result>
{
    [SwaggerSchema("ID отзыва для обновления")]
    public int Id { get; init; }

    [SwaggerSchema("Текст отзыва")]
    public string Comment { get; init; } = string.Empty;
    
    [SwaggerSchema("Описание отзыва")]
    public string Description { get; init; } = string.Empty;

    [SwaggerSchema("Оценка отзыва (1-5)")]
    public int Rating { get; init; }
}