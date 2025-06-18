using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.QueryObjects;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Queries.GetAllRoomsQuery;

[SwaggerSchema("Запрос на получение всех Room")]
public class GetAllRoomsQuery : ICachedQuery<Result<List<RoomEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => "room-list";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public IEnumerable<object> ExtractCacheableItems(Result<List<RoomEntityInfo>> response)
    {
        return response.Value?.Select(r => (object)r.Id) ?? Array.Empty<object>();
    }
}