using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class FeatureEntityInfo : IQueryEntity
{
    public string Name { get; set; }
    public bool Forcibly { get; set; }
    public int Price { get; set; }
}