using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Queries.GetAllRolesQuery;

public class GetAllRolesQuery : ICachedQuery<Result<IEnumerable<RoleEntityInfo>>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => "role-all";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => TimeSpan.FromSeconds(1);

    public IEnumerable<object> ExtractCacheableItems(Result<IEnumerable<RoleEntityInfo>> response)
    {
        return response.Value?.Select(x => (object)x.Id) ?? [];
    }
}