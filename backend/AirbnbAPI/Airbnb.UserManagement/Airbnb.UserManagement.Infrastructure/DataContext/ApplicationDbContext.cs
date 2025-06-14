using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Infrastructure.Enteties;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.UserManagement.Infrastructure.DataContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<DomainUser> Users { get; set; } = default!;
    public DbSet<DomainUserLanguage> UserLanguages { get; set; } = default!;
    public DbSet<DomainLanguage> Languages { get; set; } = default!;
    
    public DbSet<DomainRole> Roles { get; set; } = default!;
    
    public DbSet<DomainPermission> Permissions { get; set; } = default!;
    public DbSet<DomainUserRole> UserRoles { get; set; } = default!;
    public DbSet<DomainUserPermission> UserPermissions { get; set; } = default!;


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}