using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Queries.GetProductRatingByIdQuery;

public class GetProductRatingByIdQuery : ICachedQuery<Result<decimal>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"review-list-{ProductId}";
    
    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;
    public int ProductId { get; set; }
    
    public IEnumerable<object> ExtractCacheableItems(Result<decimal> response)
    {
        return [ProductId];
    }
}