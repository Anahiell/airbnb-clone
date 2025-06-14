using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.UserManagement.Infrastructure.Enteties;

public class DomainPermissionConfiguration : IEntityTypeConfiguration<DomainPermission>
{
    public void Configure(EntityTypeBuilder<DomainPermission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Permission)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(p => p.Permission).IsUnique();
    }
}