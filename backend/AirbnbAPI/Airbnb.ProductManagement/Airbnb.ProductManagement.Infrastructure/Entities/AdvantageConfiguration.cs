using Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Infrastructure.Entities;

public class AdvantageConfiguration : IEntityTypeConfiguration<Advantage>
{
    public void Configure(EntityTypeBuilder<Advantage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Description).HasMaxLength(1000);
    }
}