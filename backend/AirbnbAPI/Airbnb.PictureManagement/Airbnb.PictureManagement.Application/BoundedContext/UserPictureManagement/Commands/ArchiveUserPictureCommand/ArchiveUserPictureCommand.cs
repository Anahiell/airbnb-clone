using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.ArchiveUserPictureCommand;

/// <summary>
/// Команда для архивирования картинки пользователя
/// </summary>
[SwaggerSchema("Команда для архивирования пользовательской картинки")]
public class ArchiveUserPictureCommand : ICommand<Result>
{
    [SwaggerSchema("ID картинки пользователя")]
    public int Id { get; init; }
}