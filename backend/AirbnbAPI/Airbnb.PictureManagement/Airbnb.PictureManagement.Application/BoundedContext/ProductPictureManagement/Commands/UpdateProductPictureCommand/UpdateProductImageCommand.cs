using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands.UpdatePictureCommand;

[SwaggerSchema("Команда для обновления картинки продукта")]
public class UpdateProductImageCommand : ICommand<Result>
{
    [SwaggerSchema("ID картинки продукта")]
    public int Id { get; init; }

    [SwaggerSchema("Новый URL картинки продукта")]
    public IFormFile File { get; set; } = null!;

}