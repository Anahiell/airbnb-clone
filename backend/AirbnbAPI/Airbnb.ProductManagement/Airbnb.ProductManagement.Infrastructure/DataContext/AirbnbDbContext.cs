using System.Reflection;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.CoordinatesManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductAdditionalInfoManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address.AddressEnteties;
using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductRulesManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.ValueObjects;
using Airbnb.Domain.BoundedContexts.RoomManagement.Aggregates;
using Airbnb.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.DataContext;

public class AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : DbContext(options)
{
    public DbSet<DomainProduct> DomainProduct { get; private set; }
    public DbSet<AddressLegal> AddressLegal { get; private set; }
    public DbSet<AddressLegal> ProductType { get; private set; }

    public DbSet<ProductFacility> ProductFacilities { get; private set; }
    public DbSet<ProductFeature> ProductFeatures { get; private set; }
    
    public DbSet<Feature> Features { get; private set; }
    public DbSet<Facility> Facilities { get; private set; }
    public DbSet<Coordinate> Coordinates { get; private set; }
    public DbSet<Rule> GuestRules { get; private set; }
    public DbSet<Additional> AdditionalInfos { get; private set; }
    public DbSet<Advantage> Advantages { get; private set; }
    
    public DbSet<Room> Rooms { get; private set; }
    public DbSet<ProductRoom> ProductRooms { get; private set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}