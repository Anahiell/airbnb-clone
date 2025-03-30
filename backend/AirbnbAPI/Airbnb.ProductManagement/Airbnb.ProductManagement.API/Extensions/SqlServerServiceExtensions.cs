using System.Text;
using Airbnb.Infrastructure.Configuration;
using Airbnb.Infrastructure.DataContext;
using Airbnb.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AirbnbAPI.Extensions;

public static class SqlServerServiceExtensions
{
    public static IServiceCollection AddSqlServerServices(this IServiceCollection services, SqlServerSettings settings)
    {
        var connectionString = new SqlConnectionStringBuilder()
        {
            DataSource = $"{settings.Url},{settings.Port}",
            InitialCatalog = settings.Database,
            Password = settings.Password,
            UserID = settings.Username,
            Pooling = false,
            TrustServerCertificate = true
        };

        services.AddDbContext<AirbnbDbContext>(o =>
        {
            o.UseSqlServer
            (
                connectionString.ConnectionString,
                b => b.MigrationsAssembly(typeof(AirbnbDbContext).Assembly.FullName)
            );
            o.EnableDetailedErrors();
        });

        services.AddScoped<ProductRepository>();

        return services;
    }

    public static IApplicationBuilder UseSqlServerMigration(this IApplicationBuilder app, AirbnbDbContext context)
    {
        context.Database.Migrate();
        return app;
    }
}