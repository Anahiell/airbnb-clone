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

        builder.OwnsMany(x => x.HomeRules, a =>
        {
            a.WithOwner().HasForeignKey("AdditionalId");
            a.Property<int>("Id");
            a.HasKey("Id");
            a.Property(hr => hr.Type);
            a.Property(hr => hr.Text);
        });

        builder.OwnsMany(x => x.SafetyRules, a =>
        {
            a.WithOwner().HasForeignKey("AdditionalId");
            a.Property<int>("Id");
            a.HasKey("Id");
            a.Property(sr => sr.Type);
            a.Property(sr => sr.Label);
        });
    }
}