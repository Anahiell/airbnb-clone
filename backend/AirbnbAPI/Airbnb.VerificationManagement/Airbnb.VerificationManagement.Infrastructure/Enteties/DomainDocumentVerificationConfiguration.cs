using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.VerificationManagement.Infrastructure.Enteties;

public class DomainDocumentVerificationConfiguration : IEntityTypeConfiguration<DomainDocumentVerification>
{
    public void Configure(EntityTypeBuilder<DomainDocumentVerification> builder)
    {
        builder.ToTable("DocumentVerifications");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.UserId)
            .IsRequired();

        builder.Property(v => v.DocumentId)
            .IsRequired();

        builder.Property(v => v.SubmittedAt)
            .IsRequired();

        builder.Property(v => v.VerifiedAt)
            .IsRequired(false);

        builder.OwnsOne(v => v.Status, status =>
        {
            status.Property(s => s.Value)
                .HasColumnName("VerificationStatus")
                .IsRequired();
        });
    }
}