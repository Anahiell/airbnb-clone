using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.Queries;

[SwaggerSchema("Запрос для получения списка картинок с пагинацией")]
public class GetPicturePaginatedQuery : ICachedQuery<Result<IEnumerable<PictureEntityInfo>>>
{
    public string Key => $"picture-list-{Page}-{PageSize}";
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