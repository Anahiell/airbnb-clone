using Airbnb.MongoRepository.Entities;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;

public class PermissionEntityInfo : IQueryEntity
{
    public string Name { get; set; } = string.Empty;
}