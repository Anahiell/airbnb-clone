using System.Reflection;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.PictureManagement.Infrastructure.DataContext;

public class AirbnbPictureDbContext(DbContextOptions<AirbnbPictureDbContext> options) : DbContext(options)
{
    public DbSet<DomainPicture> DomainPicture { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}