using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.UserManagement.Infrastructure.Enteties;

public class DomainUserRoleConfiguration : IEntityTypeConfiguration<DomainUserRole>
{
    public void Configure(EntityTypeBuilder<DomainUserRole> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(u => u.Id);

        builder.HasOne<DomainUser>()
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<DomainRole>()
            .WithMany()
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}