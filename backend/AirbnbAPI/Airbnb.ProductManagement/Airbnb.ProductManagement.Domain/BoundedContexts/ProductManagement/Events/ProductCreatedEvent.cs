using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductManagement.Events;

public class ProductCreatedEvent : DomainEvent
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int Price { get; private set; }
    public bool IsAvailable { get; private set; }
    public int UserId { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public int AppartmentTypeId { get; private set; }
    public int AddressLegalId { get; private set; }

    public ProductCreatedEvent(int aggregateId, string productName, string productDescription, int productPrice,
        bool productIsAvailable, DateTime createdDate, int userId, int apartmentTypeId, int addressLegalId)
        : base(aggregateId)
    {
        Title = productName;
        Description = productDescription;
        Price = productPrice;
        IsAvailable = productIsAvailable;
        CreatedDate = createdDate;
        UserId = userId;
        AppartmentTypeId = apartmentTypeId;
        AddressLegalId = addressLegalId;
    }
}