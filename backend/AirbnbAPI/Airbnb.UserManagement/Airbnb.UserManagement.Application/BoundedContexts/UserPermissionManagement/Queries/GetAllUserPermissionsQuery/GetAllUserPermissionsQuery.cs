using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Queries.GetAllUserPermissionsQuery;


public class GetAllUserPermissionsQuery : ICachedQuery<Result<IEnumerable<UserPermissionEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => "userpermission-all";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => TimeSpan.FromSeconds(1);

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<UserPermissionEntityInfo>> response)
    {
        return response.Value?.Select(x => (object)x.Id) ?? Enumerable.Empty<object>();
    }
}