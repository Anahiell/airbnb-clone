using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class SafetyRulesEntityInfo : QueryEntity
{
    public string Type { get; set; }
    public string Label { get; set; }
}