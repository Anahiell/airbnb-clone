using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands.UpdatePictureCommand;

[SwaggerSchema("Команда для обновления картинки")]
public class UpdatePictureCommand : ICommand<Result>
{
    [SwaggerSchema("ID картинки")]
    public int Id { get; init; }

    [SwaggerSchema("Новый URL картинки")]
    public string Url { get; init; } = string.Empty;
}