using Airbnb.Domain.BoundedContexts.AddressManagement.Events;
using Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address.AddressEnteties;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;

public class AddressLegal : AggregateRoot
{
    // Регион
    public Region? Region { get; private set; }

    // Страна
    public Country? Country { get; private set; }

    // Город
    public City? City { get; private set; }

    // Улица
    public District? District { get; private set; }

    // Дом
    public House? House { get; private set; }

    // Корпус
    public Block? Block { get; private set; }

    // Квартира
    public Flat? Flat { get; private set; }

    public AddressLegal()
    {
    }

    public AddressLegal(Region region, Country country, City city, District district,
        House house, Block? block = null, Flat? flat = null)
    {
        Region = region;
        Country = country;
        City = city;
        District = district;
        House = house;
        Block = block;
        Flat = flat;
    }

    #region Aggregate Methods

    public void CreateAddress(string region, string country, string city, string district,
        string house, string? block = null, string? flat = null)
    {
        Region = new Region(region);
        Country = new Country(country);
        City = new City(city);
        District = new District(district);
        House = new House(house);
        Block = block != null ? new Block(block) : null;
        Flat = flat != null ? new Flat(flat) : null;

        RaiseEvent(new AddressLegalCreatedEvent(Id, region, country, city, 
            district, house, block, flat));
    }
    
    public void UpdateAddress(string region, string country, string city, string district,
        string house, string? block = null, string? flat = null)
    {
        Region = new Region(region);
        Country = new Country(country);
        City = new City(city);
        District = new District(district);
        House = new House(house);
        Block = block != null ? new Block(block) : null;
        Flat = flat != null ? new Flat(flat) : null;

        RaiseEvent(new AddressLegalUpdatedEvent(Id, region, country, city, district, house, block, flat));
    }

    public void DeleteAddress()
    {
        RaiseEvent(new AddressLegalDeletedEvent(Id));
    }

    #endregion

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case AddressLegalCreatedEvent e:
                OnAddressCreatedEvent(e);
                break;
            case AddressLegalUpdatedEvent e:
                OnAddressUpdatedEvent(e);
                break;
            case AddressLegalDeletedEvent e:
                OnAddressDeletedEvent(e);
                break;
        }
    }

    private void OnAddressCreatedEvent(AddressLegalCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Region = new Region(@event.Region);
        Country = new Country(@event.Country);
        City = new City(@event.City);
        District = new District(@event.District);
        House = new House(@event.House);
        Block = @event.Block != null ? new Block(@event.Block) : null;
        Flat = @event.Flat != null ? new Flat(@event.Flat) : null;
    }

    private void OnAddressUpdatedEvent(AddressLegalUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        Region = new Region(@event.Region);
        Country = new Country(@event.Country);
        City = new City(@event.City);
        District = new District(@event.District);
        House = new House(@event.House);
        Block = @event.Block != null ? new Block(@event.Block) : null;
        Flat = @event.Flat != null ? new Flat(@event.Flat) : null;
    }

    private void OnAddressDeletedEvent(AddressLegalDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }
    
    #endregion
}