using Airbnb.MongoRepository.Entities;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;

public class RoleEntityInfo : IQueryEntity
{
    public string Name { get; set; } = string.Empty;
}