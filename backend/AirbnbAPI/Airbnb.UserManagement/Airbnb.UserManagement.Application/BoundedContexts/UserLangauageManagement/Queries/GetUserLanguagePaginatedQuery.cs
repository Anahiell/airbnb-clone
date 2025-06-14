using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Queries;

public class GetUserLanguagePaginatedQuery : ICachedQuery<Result<IEnumerable<UserLanguageEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"user-language-list-{UserId}-{Page}-{PageSize}-{SortOrder}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => null;

    public int? UserId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public UserLanguageSortState SortOrder { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<UserLanguageEntityInfo>> response)
    {
        return response.Value?.Select(l => (object)l.Id) ?? [];
    }
}