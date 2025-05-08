using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.TagsManagement.Infrastructure.DataContext;


public class AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : DbContext(options)
{
    public DbSet<DomainTag> DomainTag { get; set; }
    public DbSet<ProductTag> ProductTag { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DomainTag>(entity =>
        {
            entity.ToTable("Tags");

            entity.HasKey(t => t.Id);

            entity.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(t => t.CreatedAt)
                .IsRequired();
        });
    }
}