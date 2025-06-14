using Airbnb.IntegrationTesting.Generators;
using Airbnb.IntegrationTesting.Persistance.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.IntegrationTesting.Settings;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntegrationStrategies<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        services.AddScoped<IDbContextStrategy<TDbContext>, SqliteInMemoryStrategy<TDbContext>>();
        // services.AddScoped<IDbContextStrategy<TDbContext>, MassTransitRabbitMqStrategy<TDbContext>>();
        return services;
    }
}