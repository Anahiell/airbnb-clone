using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.VerificationManagement.Infrastructure.Enteties;

public class DomainDocumentConfiguration : IEntityTypeConfiguration<DocumentDataBase>
{
    public void Configure(EntityTypeBuilder<DocumentDataBase> builder)
    {
        builder.ToTable("Documents");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.UserId).IsRequired();
        builder.Property(d => d.FilePath).IsRequired().HasMaxLength(300);
        builder.Property(d => d.UploadedAt).IsRequired();

        builder.OwnsOne(d => d.Type, type =>
        {
            type.Property(t => t.Value).HasColumnName("DocumentType").IsRequired();
        });

        builder.Property(d => d.DataJson).HasColumnName("DataJson").IsRequired();
    }
}