using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class ProductEntityInfo : IQueryEntity
{
    public string? Title { get; set; }

    public string? Description { get; set; }
    public int Price { get; set; }

    public int UserId { get; set; }

    public bool Availability { get; set; }

    public int ApartmentTypeId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int AddressLegalId { get; set; }
}