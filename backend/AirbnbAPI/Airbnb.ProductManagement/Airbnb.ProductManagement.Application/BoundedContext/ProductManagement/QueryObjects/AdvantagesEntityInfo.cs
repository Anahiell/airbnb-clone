using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class AdvantagesEntityInfo : QueryEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
}