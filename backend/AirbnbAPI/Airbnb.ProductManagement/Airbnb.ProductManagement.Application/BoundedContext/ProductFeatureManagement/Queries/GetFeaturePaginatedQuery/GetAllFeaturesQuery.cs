using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.QueryObjects;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Queries.GetFeaturePaginatedQuery;

[SwaggerSchema("Запрос на получение всех Feature")]
public class GetAllFeaturesQuery : ICachedQuery<Result<List<FeatureEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => "feature-list";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public IEnumerable<object> ExtractCacheableItems(Result<List<FeatureEntityInfo>> response)
    {
        return response.Value?.Select(p => (object)p.Id) ?? Array.Empty<object>();
    }
}