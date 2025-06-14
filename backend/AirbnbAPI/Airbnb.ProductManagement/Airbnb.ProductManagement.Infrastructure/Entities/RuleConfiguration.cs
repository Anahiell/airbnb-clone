using Airbnb.Domain.BoundedContexts.ProductRulesManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Infrastructure.Entities;

public class RuleConfiguration : IEntityTypeConfiguration<Rule>
{
    public void Configure(EntityTypeBuilder<Rule> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MaxGuestsNumber).IsRequired();
        builder.Property(x => x.PetsAllowed).IsRequired();
        builder.Property(x => x.MaxPetsNumber);
        builder.Property(x => x.PetsAddedPrice);
    }
}