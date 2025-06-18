using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.QueryObjects;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Queries.GetFacilityPaginatedQuery;

[SwaggerSchema("Запрос на получение объекта Feature по его Id")]
public class GetFeatureByIdQuery : ICachedQuery<Result<FeatureEntityInfo>>
{
    [SwaggerSchema("ID объекта Feature")]
    public int FeatureId { get; set; }

    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"feature-{FeatureId}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public IEnumerable<object> ExtractCacheableItems(Result<FeatureEntityInfo> response)
    {
        return [FeatureId];
    }
}