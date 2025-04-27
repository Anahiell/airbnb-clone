using System.Reflection;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.ReviewManagementInfrastructure.DataContext;

public class AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : DbContext(options)
{
    public DbSet<DomainReview> DomainReview { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}