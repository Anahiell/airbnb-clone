using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Infrastructure.Enteties;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.UserManagement.Infrastructure.DataContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<DomainUser> Users { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DomainUserConfiguration());
    }
}