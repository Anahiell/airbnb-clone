using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;
using Airbnb.SharedKernel;

namespace Airbnb.Domain;

public class DomainProduct : AggregateRoot
{
    public string Title { get; private set; }

    public string? Description { get; private set; }

    public int Price { get; private set; }

    public bool IsAvailable { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public int UserId { get; private set; }

    public AddressLegal AddressLegal { get; private set; }

    public int AddressLegalId { get; private set; }
    public ApartmentType ApartmentType { get; private set; }
    
    public int ApartmentTypeId { get; private set; }

    public DomainProduct()
    {
    }

    public DomainProduct(string productTitle, string productDescription, int productPrice,
         DateTime orderDate, int userId, int apartmentTypeId, int addressLegalId, bool productAvailability = true)
    {
        Title = productTitle;
        Description = productDescription;
        Price = productPrice;
        IsAvailable = productAvailability;
        CreatedAt = orderDate;
        UserId = userId;
        ApartmentTypeId = apartmentTypeId;
        AddressLegalId = addressLegalId;
    }
}