using Airbnb.MongoRepository.Entities;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;

public class UserEntityInfo : IQueryEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public PictureInfo Url { get; set; }
    public int Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class PictureInfo
{
    public string Url { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Id { get; set; }
}