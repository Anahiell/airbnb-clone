using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.PictureManagement.Infrastructure.Entities;

public class UserPictureConfiguration : IEntityTypeConfiguration<UserPicture>
{
    public void Configure(EntityTypeBuilder<UserPicture> builder)
    {
        builder.ToTable("UserPictures");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PictureGuid)
            .IsRequired();

        builder.Property(p => p.Url)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();
    }
}