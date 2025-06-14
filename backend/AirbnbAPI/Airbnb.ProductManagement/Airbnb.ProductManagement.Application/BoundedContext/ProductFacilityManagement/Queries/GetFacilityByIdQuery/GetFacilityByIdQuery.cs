using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Queries.GetFacilityByIdQuery;

[SwaggerSchema("Запрос на получение объекта Facility по его Id")]
public class GetFacilityByIdQuery : ICachedQuery<Result<FacilityEntityInfo>>
{
    [SwaggerSchema("ID объекта Facility")]
    public int FacilityId { get; set; }

    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"facility-{FacilityId}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public IEnumerable<object> ExtractCacheableItems(Result<FacilityEntityInfo> response)
    {
        return [FacilityId];
    }
}