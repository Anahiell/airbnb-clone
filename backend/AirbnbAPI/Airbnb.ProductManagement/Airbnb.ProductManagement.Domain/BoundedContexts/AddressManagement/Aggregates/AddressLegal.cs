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

    public override string ToString() =>
        $"{Region?.Value}, {Country?.Value}, {City?.Value}, {District?.Value}, {House?.Value}" +
        $"{(Block?.Value != null ? ", " + Block.Value : "")}" +
        $"{(Flat?.Value != null ? ", " + Flat.Value : "")}";
}