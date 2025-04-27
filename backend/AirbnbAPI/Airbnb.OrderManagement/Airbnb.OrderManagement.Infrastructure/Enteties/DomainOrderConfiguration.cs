using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.OrderManagement.Infrastructure.Enteties;

public class DomainOrderConfiguration : IEntityTypeConfiguration<DomainOrder>
{
    public void Configure(EntityTypeBuilder<DomainOrder> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.ProductId)
            .IsRequired();

        builder.Property(o => o.UserId)
            .IsRequired();

        builder.Property(o => o.DateStart)
            .IsRequired();

        builder.Property(o => o.DateEnd)
            .IsRequired();
    }
}