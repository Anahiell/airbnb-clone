using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries.GetProductRatingByIdQuery;

public class GetProductByIdQuery : ICachedQuery<Result<ProductEntityInfo>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"product-{ProductId}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public int ProductId { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<ProductEntityInfo> response)
    {
        return response.Value != null ? new[] { (object)response.Value.Id } : Enumerable.Empty<object>();
    }
}