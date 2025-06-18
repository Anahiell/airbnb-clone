using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class CoordinateEntityInfo : IQueryEntity
{
    public string Latitude { get; set; }

    public string Longitude { get; set; }
}