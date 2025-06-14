using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Queries.GetAllPermissionsQuery;

public class GetAllPermissionsQuery : ICachedQuery<Result<IEnumerable<PermissionEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => "permission-all";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => TimeSpan.FromSeconds(1);

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<PermissionEntityInfo>> response)
    {
        return response.Value?.Select(x => (object)x.Id) ?? [];
    }
}