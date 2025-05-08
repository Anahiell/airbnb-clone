using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.QueryObjects;
using Airbnb.TagsManagement.Application.BoundedContext.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Queries.GetProductTagsByProductIdQuery;

public class GetProductTagsPaginatedQuery : ICachedQuery<Result<IEnumerable<ProductTagEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"product-tag-list-{ProductId}-{string.Join(",", Name ?? new List<string>())}-{Page}-{PageSize}-{SortOrder}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public int ProductId { get; set; }
    public List<string>? Name { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public TagSortState SortOrder { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<ProductTagEntityInfo>> response)
    {
        return response.Value?.Select(t => (object)t.TagId) ?? [];
    }
}