using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address.AddressEnteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Infrastructure.Entities;


public class DomainProductConfiguration : IEntityTypeConfiguration<DomainProduct>
{
    public void Configure(EntityTypeBuilder<DomainProduct> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .IsRequired();

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.HasOne(p => p.AddressLegal)
            .WithMany()
            .HasForeignKey(p => p.AddressLegalId)
            .IsRequired();

        builder.HasOne(p => p.ApartmentType)
            .WithMany()
            .HasForeignKey(p => p.ApartmentTypeId)
            .IsRequired();
    }
}
