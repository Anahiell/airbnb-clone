using System.Reflection;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.OrderManagement.Infrastructure.DataContext;

public class AirbnbOrderDbContext(DbContextOptions<AirbnbOrderDbContext> options) : DbContext(options)
{
    public DbSet<DomainOrder> DomainOrder { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}