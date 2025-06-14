using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Infrastructure.Entities;

public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Forcibly).IsRequired();
        builder.Property(x => x.Price).IsRequired();
    }
}