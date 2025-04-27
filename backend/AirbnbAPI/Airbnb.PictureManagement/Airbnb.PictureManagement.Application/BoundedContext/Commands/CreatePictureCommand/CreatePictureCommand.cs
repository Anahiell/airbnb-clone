using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands;

[SwaggerSchema("Команда для создания картинки")]
public class CreatePictureCommand : ICommand<Result<int>>
{
    [SwaggerSchema("URL картинки")]
    public string Url { get; init; } = string.Empty;

    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }
}