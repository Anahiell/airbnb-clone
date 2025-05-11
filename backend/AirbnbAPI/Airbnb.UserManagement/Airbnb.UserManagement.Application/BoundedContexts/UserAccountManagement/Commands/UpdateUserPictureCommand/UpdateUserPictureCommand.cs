using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserPictureCommand;

[SwaggerSchema("Команда для обновления фотографии пользователя")]
public class UpdateUserPictureCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID пользователя")]
    public int UserId { get; init; }

    [SwaggerSchema("Файл фотографии")]
    public IFormFile Picture { get; init; } = default!;
}