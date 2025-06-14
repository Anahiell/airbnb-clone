using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Queries;


public class GetAllLanguagesQuery : ICachedQuery<Result<IEnumerable<LanguageEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => "language-all";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => TimeSpan.FromSeconds(1);

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<LanguageEntityInfo>> response)
    {
        return response.Value?.Select(x => (object)x.Id) ?? [];
    }
}