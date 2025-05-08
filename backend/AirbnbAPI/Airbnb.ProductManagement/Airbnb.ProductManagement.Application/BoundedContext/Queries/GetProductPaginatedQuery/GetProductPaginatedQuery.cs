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
    [SwaggerParameter(Description = "Начало периода (например, 2025-05-08 или 2025-05-08T14:30:00)")]
    public DateTime? DateStart { get; set; }
    
    [SwaggerParameter(Description = "Конец периода (например, 2025-05-08 или 2025-05-08T14:30:00)")]
    public DateTime? DateEnd { get; set; }

    public IEnumerable<string>? Tags { get; set; }

    public double? MinRating { get; set; }
    public double? MaxRating { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public SortState SortOrder { get; set; }
    
    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<ProductEntityInfo>> response)
    {
        return response.Value?.Select(p => (object)p.Id) ?? [];
    }
}