using Airbnb.OrderManagement.Infrastructure.Configuration;
using Airbnb.OrderManagement.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Airbnb.OrderManagement.API.Extensions;

public static class PostgreSqlExtensions
{
    public static IServiceCollection AddPostgreSqlServices(this IServiceCollection services, PostgreSqlSettings settings)
    {
        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = settings.Url,
            Port = settings.Port,
            Database = settings.Database,
            Username = settings.Username,
            Password = settings.Password,
            Pooling = true,
            TrustServerCertificate = true
        };

        services.AddDbContext<AirbnbOrderDbContext>(o =>
        {
            o.UseNpgsql
            (
                connectionString.ConnectionString,
                b => b.MigrationsAssembly(typeof(AirbnbOrderDbContext).Assembly.FullName)
            );
            o.EnableDetailedErrors();
        });

        services.AddScoped<AirbnbOrderDbContext>();

        return services;
    }

    public static IApplicationBuilder UsePostgreSqlMigration(this IApplicationBuilder app, AirbnbOrderDbContext context)
    {
        context.Database.Migrate();
        return app;
    }
}