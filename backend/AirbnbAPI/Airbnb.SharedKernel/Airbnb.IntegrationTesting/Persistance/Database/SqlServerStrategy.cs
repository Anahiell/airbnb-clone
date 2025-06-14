using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.IntegrationTesting.Persistance.Database;

public class SqlServerStrategy<TDbContext> : IDbContextStrategy<TDbContext>
    where TDbContext : DbContext
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        /*
        var connString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TDbContext>(options =>
            options.UseSqlServer(connString));
        */
    }
}