using System.Text.Json.Serialization;
using Airbnb.Application.Messaging.Cache;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Queries.GetRoleByIdQuery;

public class GetRoleByIdQuery : ICachedQuery<Result<RoleEntityInfo>>
{
    [JsonIgnore]
    [SwaggerIgnore]
    public string Key => $"role-{Id}";

    [JsonIgnore]
    [SwaggerIgnore]
    public TimeSpan? Expiration => TimeSpan.FromSeconds(1);

    [SwaggerSchema("Идентификатор роли")]
    public int Id { get; set; }

    public IEnumerable<object> ExtractCacheableItems(Result<RoleEntityInfo> response)
    {
        return response.Value is not null ? [response.Value.Id] : [];
    }
}