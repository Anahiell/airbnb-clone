using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbnb.UserManagement.Infrastructure.Enteties;

public class DomainUserConfiguration : IEntityTypeConfiguration<DomainUser>
{
    public void Configure(EntityTypeBuilder<DomainUser> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.DateOfBirth)
            .IsRequired();
        
        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();
        
        builder.Property(u => u.IsEmailVerified)
            .IsRequired();

        builder.Property(u => u.IsDocumentVerified)
            .IsRequired();
        
        // UserProfile
        builder.OwnsOne(u => u.Profile, profile =>
        {
            profile.Property(p => p.School).HasMaxLength(100).HasColumnName("School");
            profile.Property(p => p.Location).HasMaxLength(100).HasColumnName("Location");
            profile.Property(p => p.Birthdate).HasColumnName("ProfileBirthdate");
            profile.Property(p => p.Hobbies).HasMaxLength(200).HasColumnName("Hobbies");
            profile.Property(p => p.LifeGoals).HasMaxLength(200).HasColumnName("LifeGoals");
            profile.Property(p => p.TimeSpentOn).HasMaxLength(100).HasColumnName("TimeSpentOn");
            profile.Property(p => p.Profession).HasMaxLength(100).HasColumnName("Profession");
            profile.Property(p => p.FavSong).HasMaxLength(200).HasColumnName("FavSong");
            profile.Property(p => p.FunFact).HasMaxLength(200).HasColumnName("FunFact");
            profile.Property(p => p.BioTitle).HasMaxLength(200).HasColumnName("BioTitle");
            profile.Property(p => p.Pets).HasMaxLength(100).HasColumnName("Pets");
            profile.Property(p => p.About).HasMaxLength(500).HasColumnName("About");
        });
        
        builder
            .HasMany(u => u.Languages)
            .WithOne(ul => ul.User)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(u => u.UserRoles)
            .WithOne()
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.UserPermissions)
            .WithOne()
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}