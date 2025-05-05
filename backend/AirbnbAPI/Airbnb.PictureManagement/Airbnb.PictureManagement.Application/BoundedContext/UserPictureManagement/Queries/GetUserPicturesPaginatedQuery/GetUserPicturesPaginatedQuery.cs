using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Queries.GetUserPicturesPaginatedQuery;

[SwaggerSchema("Запрос для получения списка картинок пользователя с пагинацией")]
public class GetUserPicturesPaginatedQuery : ICachedQuery<Result<IEnumerable<UserPictureEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"user-picture-list-{UserId}-{Page}-{PageSize}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public int UserId { get; set; }
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }
    public PictureSortState SortOrder { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<UserPictureEntityInfo>> response)
    {
        return response.Value?.Select(p => (object)p.Id) ?? [];
    }
}