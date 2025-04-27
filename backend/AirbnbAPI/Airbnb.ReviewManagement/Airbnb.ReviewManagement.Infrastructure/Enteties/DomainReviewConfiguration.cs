using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.ReviewManagementInfrastructure.Enteties;

public class DomainReviewConfiguration : IEntityTypeConfiguration<DomainReview>
{
    public void Configure(EntityTypeBuilder<DomainReview> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(r => r.Description)
            .HasMaxLength(1000);

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.ProductId)
            .IsRequired();
    }
}