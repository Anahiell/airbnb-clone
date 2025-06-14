using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.UserManagement.Infrastructure.Enteties;

public class DomainUserPermissionConfiguration : IEntityTypeConfiguration<DomainUserPermission>
{
    public void Configure(EntityTypeBuilder<DomainUserPermission> builder)
    {
        builder.ToTable("UserPermissions");

        builder.HasKey(u => u.Id);

        builder.HasOne<DomainUser>()
            .WithMany(u => u.UserPermissions)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<DomainPermission>()
            .WithMany()
            .HasForeignKey(up => up.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}