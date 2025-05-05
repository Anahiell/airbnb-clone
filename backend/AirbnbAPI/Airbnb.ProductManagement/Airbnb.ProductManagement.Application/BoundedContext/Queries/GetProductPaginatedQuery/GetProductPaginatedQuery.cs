using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.Domain;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries;

public class GetProductPaginatedQuery : ICachedQuery<Result<IEnumerable<ProductEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"product-list-{Page}-{PageSize}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public string? Country { get; set; }
    public string? City { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public IEnumerable<object>? Tags { get; set; }
    public int MinRating { get; set; }
    public int MaxRating { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public SortState SortOrder { get; set; }
    
    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<ProductEntityInfo>> response)
    {
        return response.Value?.Select(p => (object)p.Id) ?? [];
    }
}