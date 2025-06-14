using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Queries;

public class GetLanguageByIdQuery : ICachedQuery<Result<LanguageEntityInfo>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"language-{Id}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => TimeSpan.FromSeconds(1);

    [SwaggerSchema("Идентификатор языка")]
    public int Id { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<LanguageEntityInfo> response)
    {
        return response.Value is not null ? [response.Value.Id] : [];
    }
}