using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Interfaces;
using Airbnb.ReviewManagementInfrastructure.Configuration;
using Airbnb.ReviewManagementInfrastructure.DataContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.TagManagement.API.Extensions;

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

        services.AddScoped<IReviewRepository>();

        return services;
    }

    public static IApplicationBuilder UseSqlServerMigration(this IApplicationBuilder app, AirbnbDbContext context)
    {
        context.Database.Migrate();
        return app;
    }
}