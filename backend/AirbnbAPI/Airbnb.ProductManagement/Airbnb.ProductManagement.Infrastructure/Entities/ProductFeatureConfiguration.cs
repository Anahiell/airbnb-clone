using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Infrastructure.Entities;

public class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
{
    public void Configure(EntityTypeBuilder<ProductFeature> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.FeatureId).IsRequired();
    }
}