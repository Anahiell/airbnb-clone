using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address.AddressEnteties;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.ValueObjects;
using Airbnb.Infrastructure.DataContext;

namespace AirbnbAPI.Extensions;

public static class DataSeeder
{
    public static void Seed(AirbnbDbContext context)
    {
        if (!context.Set<AddressLegal>().Any())
        {
            context.Set<AddressLegal>().AddRange(
                new AddressLegal(
                    new Region("Europe"),
                    new Country("France"),
                    new City("Paris"),
                    new District("Montmartre"),
                    new House("789"),
                    new Block("C"),
                    new Flat("32E")
                ),
                new AddressLegal(
                    new Region("Asia"),
                    new Country("Japan"),
                    new City("Tokyo"),
                    new District("Shinjuku"),
                    new House("456"),
                    new Block(null),
                    new Flat("8F")),
                new AddressLegal(
                    new Region("North America"),
                    new Country("Canada"),
                    new City("Toronto"),
                    new District("Downtown"),
                    new House("123"),
                    new Block("A"),
                    new Flat("12B"))
            );
            
            context.SaveChanges();
        }
        
        if (!context.Set<ApartmentType>().Any())
        {
            context.Set<ApartmentType>().AddRange(
                new ApartmentType(PropertyTypeEnum.Apartment),
                new ApartmentType(PropertyTypeEnum.House),
                new ApartmentType(PropertyTypeEnum.Townhouse)
            );
            
            context.SaveChanges();
        }

        if (!context.Set<DomainProduct>().Any())
        {
            var addresses = context.Set<AddressLegal>().Take(3).ToList();
            var types = context.Set<ApartmentType>().Take(3).ToList();

            
            context.Set<DomainProduct>().AddRange(
                new DomainProduct("Cozy Apartment", "Nice place", 100, DateTime.Now, 1,
                    types[0].Id, addresses[0].Id),
                new DomainProduct("Luxury Villa", "Big villa with pool", 300, DateTime.Now, 2,
                    types[1].Id, addresses[1].Id),
                new DomainProduct("Small Studio", "Cheap and cozy", 50, DateTime.Now, 3,
                    types[2].Id, addresses[2].Id)
            );
        }

        context.SaveChanges();
    }
}