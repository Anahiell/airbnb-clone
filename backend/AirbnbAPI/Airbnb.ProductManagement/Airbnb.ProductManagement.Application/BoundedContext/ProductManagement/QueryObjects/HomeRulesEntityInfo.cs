using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class HomeRulesEntityInfo : QueryEntity
{
    public string Type { get; set; }
    public string Text { get; set; }
}