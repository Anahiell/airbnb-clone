using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.Commands.ArchiveProductPictureCommand;

/// <summary>
/// Команда для архивирования картинки продукта.
/// </summary>
[SwaggerSchema("Команда для архивирования картинки продукта")]
public class ArchiveProductPictureCommand : ICommand<Result>
{
    /// <summary>
    /// ID картинки для архивирования
    /// </summary>
    [SwaggerSchema("ID картинки для архивирования")]
    public int Id { get; init; }
}