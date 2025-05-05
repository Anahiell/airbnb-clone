using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.Queries;

[SwaggerSchema("Запрос для получения списка картинок с пагинацией")]
public class GetProductPicturesPaginatedQuery : ICachedQuery<Result<IEnumerable<PictureEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"product-picture-list-{Page}-{PageSize}";
    
    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public int? UserId { get; set; }
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }
    public PictureSortState SortOrder { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<PictureEntityInfo>> response)
    {
        return response.Value?.Select(p => (object)p.Id) ?? [];
    }
}