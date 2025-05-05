using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.UpdateUserPictureCommand;

[SwaggerSchema("Команда для обновления картинки пользователя")]
public class UpdateUserPictureCommand : ICommand<Result>
{
    [SwaggerSchema("ID картинки для обновления")]
    public int Id { get; init; }

    [SwaggerSchema("Файл изображения для обновления")]
    public IFormFile File { get; init; }
}