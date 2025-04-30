using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.UserManagement.Infrastructure.Enteties;

public class DomainUserConfiguration : IEntityTypeConfiguration<DomainUser>
{
    public void Configure(EntityTypeBuilder<DomainUser> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Roles)
            .IsRequired();

        builder.Property(u => u.DateOfBirth)
            .IsRequired();
        
        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}