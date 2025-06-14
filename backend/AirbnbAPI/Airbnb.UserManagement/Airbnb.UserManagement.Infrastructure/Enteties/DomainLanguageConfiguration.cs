using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.UserManagement.Infrastructure.Enteties;

public class DomainLanguageConfiguration : IEntityTypeConfiguration<DomainLanguage>
{
    public void Configure(EntityTypeBuilder<DomainLanguage> builder)
    {
        builder.ToTable("Languages");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(l => l.Name)
            .IsUnique();
    }
}