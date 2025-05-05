using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Queries;

public class GetUserPaginatedQuery : ICachedQuery<Result<IEnumerable<UserEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"user-list-{Page}-{PageSize}";
    
    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public int? Role { get; set; }
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public UserSortState SortOrder { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<UserEntityInfo>> response)
    {
        return response.Value?.Select(r => (object)r.Id) ?? new List<object>();
    }
}