using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.Queries.GetProductPictureByIdQuery;

/// <summary>
/// Запрос на получение картинки продукта по ProductId.
/// </summary>
[SwaggerSchema("Запрос на получение картинки продукта по ProductId")]
public class GetProductPictureByIdQuery : IQuery<Result<PictureEntityInfo>>
{
    /// <summary>
    /// ID продукта, для которого нужно получить картинку
    /// </summary>
    [SwaggerSchema("ID продукта")]
    public int ProductId { get; init; }
}