using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.QueryObjects;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Queries.GetRoomByIdQuery;

[SwaggerSchema("Запрос на получение объекта Room по его Id")]
public class GetRoomByIdQuery : ICachedQuery<Result<RoomEntityInfo>>
{
    [SwaggerSchema("ID объекта Room")]
    public int RoomId { get; set; }

    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"room-{RoomId}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public IEnumerable<object> ExtractCacheableItems(Result<RoomEntityInfo> response)
    {
        return new object[] { RoomId };
    }
}