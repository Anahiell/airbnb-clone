using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Infrastructure.Entities;

public class ProductRoomConfiguration : IEntityTypeConfiguration<ProductRoom>
{
    public void Configure(EntityTypeBuilder<ProductRoom> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.RoomId).IsRequired();
    }
}