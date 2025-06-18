using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.QueryObjects;

public class FeatureEntityInfo : IQueryEntity
{
    public string Name { get; set; } = string.Empty;
    public bool Forcibly { get; set; }
    public int Price { get; set; }
}