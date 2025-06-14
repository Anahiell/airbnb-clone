using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.UserManagement.Infrastructure.Enteties;

public class DomainRoleConfiguration : IEntityTypeConfiguration<DomainRole>
{
    public void Configure(EntityTypeBuilder<DomainRole> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(r => r.Name).IsUnique();
    }
}