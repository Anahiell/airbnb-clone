using Airbnb.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.DataContext;

public class AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : DbContext(options)
{
    public DbSet<PropertyEntity> PropertyEntity { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PropertyEntity>().HasKey(o => o.Id);
        
        modelBuilder.Entity<PropertyEntity>().HasData(
            new PropertyEntity { Id = 1 },
            new PropertyEntity { Id = 2 },
            new PropertyEntity { Id = 3 }
        );
    }
}