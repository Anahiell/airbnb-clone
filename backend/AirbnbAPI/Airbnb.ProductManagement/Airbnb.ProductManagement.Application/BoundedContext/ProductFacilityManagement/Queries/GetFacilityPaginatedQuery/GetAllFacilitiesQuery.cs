using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Queries.GetFacilityPaginatedQuery;

[SwaggerSchema("Запрос на получение всех Facility")]
public class GetAllFacilitiesQuery : ICachedQuery<Result<List<FacilityEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => "facility-list";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public IEnumerable<object> ExtractCacheableItems(Result<List<FacilityEntityInfo>> response)
    {
        return response.Value?.Select(p => (object)p.Id) ?? [];
    }
}