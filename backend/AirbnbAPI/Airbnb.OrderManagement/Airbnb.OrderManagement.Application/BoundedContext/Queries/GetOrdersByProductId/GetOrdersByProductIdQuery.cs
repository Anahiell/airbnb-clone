using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.OrderManagement.Application.BoundedContext.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.OrderManagement.Application.BoundedContext.Queries.GetOrdersByProductId;

public class GetOrdersByProductIdQuery : ICachedQuery<Result<IEnumerable<OrderEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"order-list-{ProductId}";
    
    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public int ProductId { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<OrderEntityInfo>> response)
    {
        return response.Value?.Select(o => (object)o.Id) ?? Enumerable.Empty<object>();
    }
}