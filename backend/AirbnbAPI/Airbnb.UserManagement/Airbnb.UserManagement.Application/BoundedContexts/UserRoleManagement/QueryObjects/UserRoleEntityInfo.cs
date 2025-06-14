using Airbnb.MongoRepository.Entities;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.QueryObjects;

public class UserRoleEntityInfo : QueryEntity
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}