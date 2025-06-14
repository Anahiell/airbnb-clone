using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.IntegrationTesting.Persistance.Database;

public class SqliteInMemoryStrategy<TDbContext> : IDbContextStrategy<TDbContext>, IDisposable
    where TDbContext : DbContext
{
    private SqliteConnection? _connection;

    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        services.AddDbContext<TDbContext>(options =>
        {
            options.UseSqlite(_connection);
        });

        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}