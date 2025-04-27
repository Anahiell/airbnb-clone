using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.TagsManagement.Application.BoundedContext.QueryObjects;

namespace Airbnb.TagsManagement.Application.BoundedContext.Queries.GetTagPaginatedQuery;

public class GetTagPaginatedQuery : ICachedQuery<Result<IEnumerable<TagEntityInfo>>>
{
    public string Key => $"tag-list-{Page}-{PageSize}";
    public TimeSpan? Expiration => null;

    public string? Name { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public TagSortState SortOrder { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<TagEntityInfo>> response)
    {
        return response.Value?.Select(t => (object)t.Id) ?? [];
    }
}