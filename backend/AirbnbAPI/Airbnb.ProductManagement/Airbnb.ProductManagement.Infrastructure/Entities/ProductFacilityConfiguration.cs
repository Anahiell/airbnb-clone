using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Infrastructure.Entities;

public class ProductFacilityConfiguration : IEntityTypeConfiguration<ProductFacility>
{
    public void Configure(EntityTypeBuilder<ProductFacility> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.FacilityId).IsRequired();
    }
}