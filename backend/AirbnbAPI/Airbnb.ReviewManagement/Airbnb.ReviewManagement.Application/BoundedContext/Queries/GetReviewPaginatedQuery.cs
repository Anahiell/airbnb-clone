using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.ReviewManagement.Application.BoundedContext.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Queries;

public class GetReviewPaginatedQuery : ICachedQuery<Result<IEnumerable<ReviewEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"review-list-{Page}-{PageSize}";
    
    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public int? MinRating { get; set; }
    public int? MaxRating { get; set; }
    public int? UserId { get; set; }
    public int? ProductId { get; set; }
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public ReviewSortState SortOrder { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<ReviewEntityInfo>> response)
    {
        return response.Value?.Select(r => (object)r.Id) ?? [];
    }
}