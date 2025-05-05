using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands;

[SwaggerSchema("Команда загрузки картинок продукта")]
public class UploadProductPictureCommand : ICommand<Result<List<int>>>
{
    [SwaggerSchema("Файлы изображений")]
    public List<IFormFile> Files { get; init; } = new();

    [SwaggerSchema("ID продукта (если картинки продукта)")]
    public int ProductId { get; init; }
}