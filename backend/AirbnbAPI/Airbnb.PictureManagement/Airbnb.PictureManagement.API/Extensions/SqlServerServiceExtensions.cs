using System.Text;
using Airbnb.PictureManagement.Infrastructure.Configuration;
using Airbnb.PictureManagement.Infrastructure.DataContext;
using Airbnb.PictureManagement.Infrastructure.Repositories;
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

        services.AddDbContext<AirbnbPictureDbContext>(o =>
        {
            o.UseSqlServer
            (
                connectionString.ConnectionString,
                b => b.MigrationsAssembly(typeof(AirbnbPictureDbContext).Assembly.FullName)
            );
            o.EnableDetailedErrors();
        });

        services.AddScoped<PictureRepository>();

        return services;
    }

    public static IApplicationBuilder UseSqlServerMigration(this IApplicationBuilder app, AirbnbPictureDbContext context)
    {
        context.Database.Migrate();
        return app;
    }
}