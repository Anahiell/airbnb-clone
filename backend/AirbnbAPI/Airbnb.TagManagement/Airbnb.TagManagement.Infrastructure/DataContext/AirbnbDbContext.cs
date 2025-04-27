using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.TagsManagement.Infrastructure.DataContext;


public class AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : DbContext
{
    public DbSet<DomainTag> DomainTag { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DomainTag>().ToTable("Tags");

        modelBuilder.Entity<DomainTag>()
            .HasIndex(t => t.Name)
            .IsUnique();
    }
}