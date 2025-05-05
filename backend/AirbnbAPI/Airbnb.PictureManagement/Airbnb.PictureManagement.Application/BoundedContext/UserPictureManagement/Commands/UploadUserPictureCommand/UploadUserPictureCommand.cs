using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.UploadUserPictureCommand;

public class UploadUserPictureCommand : ICommand<Result<List<int>>>
{
    [SwaggerSchema("Файл изображения для пользователя")]
    public IFormFile File { get; init; }

    [SwaggerSchema("ID пользователя для привязки картинки")]
    public int UserId { get; init; }
}