using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.OrderManagement.Application.BoundedContext.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.OrderManagement.Application.BoundedContext.Queries.GetProductPaginatedQuery;

public class GetOrderPaginatedQuery : ICachedQuery<Result<IEnumerable<OrderEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"order-list-{Page}-{PageSize}";
    
    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public int? ProductId { get; set; }
    public int? UserId { get; set; }
    public DateTime? DateStartAfter { get; set; }
    public DateTime? DateEndBefore { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public OrderSortState SortOrder { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<OrderEntityInfo>> response)
    {
        return response.Value?.Select(o => (object)o.Id) ?? [];
    }
}