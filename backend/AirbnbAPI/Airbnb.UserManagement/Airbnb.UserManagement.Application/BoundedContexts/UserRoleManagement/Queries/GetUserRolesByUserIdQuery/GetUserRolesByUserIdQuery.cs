using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Queries.GetUserRolesByUserIdQuery;

public class GetUserRolesByUserIdQuery : ICachedQuery<Result<IEnumerable<UserRoleEntityInfo>>>
{
    [SwaggerSchema("ID пользователя")]
    public int UserId { get; set; }

    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"userroles-{UserId}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => TimeSpan.FromSeconds(1);

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<UserRoleEntityInfo>> response)
    {
        return response.Value?.Select(x => (object)$"{x.UserId}-{x.RoleId}") ?? [];
    }
}