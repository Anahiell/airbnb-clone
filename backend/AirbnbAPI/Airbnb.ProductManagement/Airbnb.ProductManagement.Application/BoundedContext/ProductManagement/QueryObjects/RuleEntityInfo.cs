using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class RuleEntityInfo : QueryEntity
{
    public int MaxGuestsNumber { get; set; }
    public bool PetsAllowed { get; set; }
    public int? MaxPetsNumber { get; set; }
    public int? PetsAddedPrice { get; set; }
}