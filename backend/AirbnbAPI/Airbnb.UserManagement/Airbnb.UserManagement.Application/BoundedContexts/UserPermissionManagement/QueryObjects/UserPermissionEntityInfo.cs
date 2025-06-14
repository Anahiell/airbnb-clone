using Airbnb.MongoRepository.Entities;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.QueryObjects;

public class UserPermissionEntityInfo : QueryEntity
{
    public int UserId { get; set; }
    public int PermissionId { get; set; }
}