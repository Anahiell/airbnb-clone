using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class AdvantagesEntityInfo : IQueryEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
}