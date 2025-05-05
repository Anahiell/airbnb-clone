using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.PictureManagement.Infrastructure.Entities;

public class ProductPictureConfiguration : IEntityTypeConfiguration<ProductPicture>
{
    public void Configure(EntityTypeBuilder<ProductPicture> builder)
    {
        builder.ToTable("ProductPictures");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PictureGuid)
            .IsRequired();

        builder.Property(p => p.Url)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.ProductId)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();
    }
}