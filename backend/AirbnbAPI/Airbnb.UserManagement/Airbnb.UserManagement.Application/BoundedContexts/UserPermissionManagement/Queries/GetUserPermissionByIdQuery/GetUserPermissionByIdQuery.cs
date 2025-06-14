using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Queries.GetUserPermissionByIdQuery;

public class GetUserPermissionByIdQuery : ICachedQuery<Result<UserPermissionEntityInfo>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"userpermission-{Id}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => TimeSpan.FromSeconds(1);

    [SwaggerSchema("Идентификатор связи")]
    public int Id { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<UserPermissionEntityInfo> response)
    {
        return response.Value is not null ? new object[] { response.Value.Id } : Enumerable.Empty<object>();
    }
}