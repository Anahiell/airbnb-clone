using System.Reflection;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address.AddressEnteties;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.ValueObjects;
using Airbnb.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.DataContext;

public class AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : DbContext(options)
{
    public DbSet<DomainProduct> DomainProduct { get; private set; }
    public DbSet<AddressLegal> AddressLegal { get; private set; }
    public DbSet<AddressLegal> ProductType { get; private set; }
    public DbSet<UserEntity> Users { get; private set; } // add Users table

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //add unique email in table Users
        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}