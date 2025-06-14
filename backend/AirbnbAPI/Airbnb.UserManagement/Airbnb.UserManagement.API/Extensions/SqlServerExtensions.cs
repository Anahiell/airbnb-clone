using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Interfaces;
using Airbnb.UserManagement.Infrastructure.Configuration;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Airbnb.UserManagement.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AirbnbAPI.Extensions;

public static class SqlServerExtensions
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
            TrustServerCertificate = true,
            ConnectRetryCount = 5,
        };

        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseSqlServer(
                connectionString.ConnectionString,
                b =>
                {
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    b.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    );
                });
            o.EnableDetailedErrors();
        });

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IApplicationBuilder UseSqlServerMigration(this IApplicationBuilder app, ApplicationDbContext context)
    {
        context.Database.Migrate();
        return app;
    }
}