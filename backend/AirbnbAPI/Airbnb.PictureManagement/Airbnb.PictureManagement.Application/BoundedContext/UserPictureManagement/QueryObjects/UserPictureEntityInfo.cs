using Airbnb.MongoRepository.Entities;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.QueryObjects;

public class UserPictureEntityInfo : IQueryEntity
{
    public string Url { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}