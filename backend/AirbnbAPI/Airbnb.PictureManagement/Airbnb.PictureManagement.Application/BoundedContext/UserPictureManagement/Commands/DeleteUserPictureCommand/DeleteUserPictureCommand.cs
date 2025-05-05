using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.DeleteUserPictureCommand;

[SwaggerSchema("Команда для удаления картинки пользователя")]
public class DeleteUserPictureCommand : ICommand<Result>
{
    [SwaggerSchema("ID картинки для удаления")]
    public int Id { get; init; }
}