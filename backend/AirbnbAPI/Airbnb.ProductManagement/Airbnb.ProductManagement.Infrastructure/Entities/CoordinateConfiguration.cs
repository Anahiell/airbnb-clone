using Airbnb.Domain.BoundedContexts.CoordinatesManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.Infrastructure.Entities;

public class CoordinateConfiguration : IEntityTypeConfiguration<Coordinate>
{
    public void Configure(EntityTypeBuilder<Coordinate> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Latitude).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Longitude).IsRequired().HasMaxLength(100);
    }
}