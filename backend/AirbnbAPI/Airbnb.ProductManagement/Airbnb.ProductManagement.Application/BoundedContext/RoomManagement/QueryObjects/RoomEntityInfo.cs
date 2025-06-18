using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.QueryObjects;

public class RoomEntityInfo : IQueryEntity
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
}