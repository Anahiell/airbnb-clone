using Airbnb.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.DataContext;

public class AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : DbContext(options)
{
    public DbSet<PropertyEntity> PropertyEntity { get; set; }
    public DbSet<UserEntity> Users { get; set; } // add Users table

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PropertyEntity>().HasKey(o => o.Id);
        modelBuilder.Entity<PropertyEntity>()
        .Property(p => p.Price)
        .HasColumnType("decimal(18,2)"); // Указываем точность

        modelBuilder.Entity<PropertyEntity>().HasData(
     new PropertyEntity { Id = 1, Title = "Cozy Apartment", Description = "Nice place", Price = 100, Location = "New York" },
     new PropertyEntity { Id = 2, Title = "Luxury Villa", Description = "Big villa with pool", Price = 300, Location = "Los Angeles" },
     new PropertyEntity { Id = 3, Title = "Small Studio", Description = "Cheap and cozy", Price = 50, Location = "San Francisco" }
 );



        //add unique email in table Users
        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}