using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.Commands.UploadProductImage;

public class UploadProductImageCommand : ICommand<Result<string>>
{
    [SwaggerSchema("ID продукта")]
    public int ProductId { get; set; }

    [SwaggerSchema("Фотографии")]
    public List<IFormFile> Images { get; set; } = new();

    [SwaggerSchema("Назначения фотографий (например: 'комната', 'кухня')")]
    public List<string> ImagesNames { get; set; } = new();
}