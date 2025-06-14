using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.UserManagement.Infrastructure.Enteties;

public class DomainUserLanguageConfiguration : IEntityTypeConfiguration<DomainUserLanguage>
{
    public void Configure(EntityTypeBuilder<DomainUserLanguage> builder)
    {
        builder.ToTable("UserLanguages");

        builder.HasKey(ul => ul.Id);

        builder.Property(ul => ul.UserId).IsRequired();
        builder.Property(ul => ul.LanguageId).IsRequired();

        builder
            .HasOne(ul => ul.Language)
            .WithMany()
            .HasForeignKey(ul => ul.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(ul => ul.User)
            .WithMany(u => u.Languages)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}