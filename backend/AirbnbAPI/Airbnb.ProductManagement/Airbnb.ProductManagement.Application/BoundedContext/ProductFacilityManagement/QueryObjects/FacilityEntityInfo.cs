using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;

public class FacilityEntityInfo : IQueryEntity
{
    public string Name { get; set; } = default!;
    public string IconName { get; set; } = default!;
}