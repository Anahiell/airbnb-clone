using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Infrastructure.Entities;

public class DomainAddressConfiguration : IEntityTypeConfiguration<AddressLegal>
{
    public void Configure(EntityTypeBuilder<AddressLegal> builder)
    {
        builder.HasKey(a => a.Id);

        builder.OwnsOne(a => a.Region, region =>
        {
            region.Property(r => r.Value)
                .HasColumnName("Region")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(a => a.Country, country =>
        {
            country.Property(c => c.Value)
                .HasColumnName("Country")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(a => a.City, city =>
        {
            city.Property(c => c.Value)
                .HasColumnName("City")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(a => a.District, district =>
        {
            district.Property(d => d.Value)
                .HasColumnName("District")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(a => a.House, house =>
        {
            house.Property(h => h.Value)
                .HasColumnName("House")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.OwnsOne(a => a.Block, block =>
        {
            block.Property(b => b.Value)
                .HasColumnName("Block")
                .HasMaxLength(50)
                .IsRequired(false);
        });

        builder.OwnsOne(a => a.Flat, flat =>
        {
            flat.Property(f => f.Value)
                .HasColumnName("Flat")
                .HasMaxLength(50)
                .IsRequired(false);
        });
    }
}