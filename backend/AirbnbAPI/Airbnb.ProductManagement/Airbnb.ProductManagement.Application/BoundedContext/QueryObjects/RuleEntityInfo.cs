using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class RuleEntityInfo : QueryEntity
{
    public int MaxGuestsNumber { get; private set; }
    public bool PetsAllowed { get; private set; }
    public int? MaxPetsNumber { get; private set; }
    public int? PetsAddedPrice { get; private set; }
}