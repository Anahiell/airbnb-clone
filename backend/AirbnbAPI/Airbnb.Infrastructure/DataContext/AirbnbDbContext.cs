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

        modelBuilder.Entity<PropertyEntity>().HasData(
            new PropertyEntity { Id = 1 },
            new PropertyEntity { Id = 2 },
            new PropertyEntity { Id = 3 }
        );


        //add unique email in table Users
        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}