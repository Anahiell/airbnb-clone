using Airbnb.Domain.BoundedContexts.ProductAdditionalInfoManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Infrastructure.Entities;

public class AdditionalConfiguration : IEntityTypeConfiguration<Additional>
{
    public void Configure(EntityTypeBuilder<Additional> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.CancelPolicy);

        builder.OwnsMany(x => x.HomeRules, b =>
        {
            b.WithOwner().HasForeignKey("AdditionalId");
            b.Property(p => p.Id).ValueGeneratedNever();
            b.HasKey(p => p.Id);
        });

        builder.OwnsMany(x => x.SafetyRules, b =>
        {
            b.WithOwner().HasForeignKey("AdditionalId");
            b.Property(p => p.Id).ValueGeneratedNever();
            b.HasKey(p => p.Id);
        });
    }
}