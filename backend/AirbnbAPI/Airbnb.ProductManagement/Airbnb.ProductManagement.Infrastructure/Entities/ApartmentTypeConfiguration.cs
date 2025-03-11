using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Infrastructure.Entities;

public class ApartmentTypeConfiguration : IEntityTypeConfiguration<ApartmentType>
{
    public void Configure(EntityTypeBuilder<ApartmentType> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Value)
            .HasColumnName("PropertyType")
            .HasConversion<string>()
            .IsRequired();
    }
}