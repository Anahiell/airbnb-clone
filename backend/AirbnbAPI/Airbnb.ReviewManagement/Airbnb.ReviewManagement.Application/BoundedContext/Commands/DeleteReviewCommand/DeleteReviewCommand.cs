using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Commands.DeleteReviewCommand;

[SwaggerSchema("Команда для удаления отзыва")]
public class DeleteReviewCommand : ICommand<Result>
{
    [SwaggerSchema("ID отзыва для удаления")]
    public int Id { get; init; }
}