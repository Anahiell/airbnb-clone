using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.ReviewManagement.Application.BoundedContext.QueryObjects;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Queries;

public class GetReviewPaginatedQuery : ICachedQuery<Result<IEnumerable<ReviewEntityInfo>>>
{
    public string Key => $"review-list-{Page}-{PageSize}";
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