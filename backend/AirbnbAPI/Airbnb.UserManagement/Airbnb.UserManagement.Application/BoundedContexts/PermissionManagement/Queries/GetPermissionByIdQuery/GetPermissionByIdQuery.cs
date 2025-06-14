using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Queries;

public class GetPermissionByIdQuery : ICachedQuery<Result<PermissionEntityInfo>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"permission-{Id}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => TimeSpan.FromSeconds(1);

    [SwaggerSchema("Идентификатор permission")]
    public int Id { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<PermissionEntityInfo> response)
    {
        return response.Value is not null ? [response.Value.Id] : [];
    }
}