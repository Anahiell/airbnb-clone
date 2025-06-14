using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.VerificationManagement.Infrastructure.DataContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<DocumentDataBase> Documents { get; set; } = default!;


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}