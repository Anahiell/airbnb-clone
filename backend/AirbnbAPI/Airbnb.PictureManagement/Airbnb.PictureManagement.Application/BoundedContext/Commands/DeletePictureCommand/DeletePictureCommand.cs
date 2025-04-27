using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands.DeletePictureCommand;

[SwaggerSchema("Команда для удаления картинки")]
public class DeletePictureCommand : ICommand<Result>
{
    [SwaggerSchema("ID картинки для удаления")]
    public int Id { get; init; }
}