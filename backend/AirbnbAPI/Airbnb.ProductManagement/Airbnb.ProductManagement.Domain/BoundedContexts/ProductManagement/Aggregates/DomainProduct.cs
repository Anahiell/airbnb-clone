using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
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
        
        RaiseEvent(new ProductCreatedEvent(Id, productTitle, productDescription, productPrice,
            productAvailability, orderDate, userId, apartmentTypeId, addressLegalId));
    }

    #region Aggregate Methods

    public void CreateProduct(string productTitle, string productDescription, int productPrice,
        bool productIsAvailable, DateTime createdDate, int userId, int appartmentTypeId, int addressLegalId)
    {
        Title = productTitle;
        Description = productDescription;
        Price = productPrice;
        IsAvailable = productIsAvailable;
        CreatedAt = createdDate;
        UserId = userId;
        ApartmentTypeId = appartmentTypeId;
        AddressLegalId = addressLegalId;

        RaiseEvent(new ProductCreatedEvent(Id, productTitle, productDescription, productPrice,
            productIsAvailable, createdDate, userId, appartmentTypeId, addressLegalId));
    }

    public void DeleteProduct()
    {
        RaiseEvent(new ProductDeletedEvent(Id));
    }

    public void UpdateProduct(string productTitle, string productDescription, int productPrice,
        bool productIsAvailable, DateTime createdDate, int userId, int appartmentTypeId, int addressLegalId)
    {
        Title = productTitle;
        Description = productDescription;
        Price = productPrice;
        IsAvailable = productIsAvailable;
        CreatedAt = createdDate;
        UserId = userId;
        ApartmentTypeId = appartmentTypeId;
        AddressLegalId = addressLegalId;

        RaiseEvent(new ProductUpdatedEvent(Id, productTitle, productDescription, productPrice,
            productIsAvailable, createdDate, userId, appartmentTypeId, addressLegalId));
    }

    #endregion

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case ProductCreatedEvent e:
                OnProductCreatedEvent(e);
                break;
            case ProductDeletedEvent e:
                OnProductDeletedEvent(e);
                break;
            case ProductUpdatedEvent e:
                OnProductUpdatedEvent(e);
                break;
        }
    }

    private void OnProductCreatedEvent(ProductCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Title = @event.Title;
        Description = @event.Description;
        Price = @event.Price;
        IsAvailable = @event.IsAvailable;
        UserId = @event.UserId;
        CreatedAt = @event.CreatedDate;
        ApartmentTypeId = @event.AppartmentTypeId;
        AddressLegalId = @event.AddressLegalId;
    }

    private void OnProductUpdatedEvent(ProductUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        Title = @event.Title;
        Description = @event.Description;
        Price = @event.Price;
        IsAvailable = @event.IsAvailable;
        CreatedAt = @event.CreatedDate;
        UserId = @event.UserId;
        ApartmentTypeId = @event.AppartmentTypeId;
        AddressLegalId = @event.AddressLegalId;
    }

    private void OnProductDeletedEvent(ProductDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion
}