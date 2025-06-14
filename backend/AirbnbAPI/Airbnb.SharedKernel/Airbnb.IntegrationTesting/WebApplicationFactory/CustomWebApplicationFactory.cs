using Airbnb.IntegrationTesting.Generators;
using Airbnb.IntegrationTesting.Persistance.Database;
using Airbnb.IntegrationTesting.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.IntegrationTesting.WebApplicationFactory;

public class CustomWebApplicationFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>
    where TProgram : class
    where TDbContext : DbContext
{
    public bool UseInMemoryDb { get; set; } = false;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTesting");

        builder.ConfigureServices((ctx, services) =>
        {
            // Удаляем старую регистрацию DbContextOptions<TDbContext>
            var descriptors = services.Where(d =>
                d.ServiceType == typeof(DbContextOptions<TDbContext>) ||
                d.ServiceType == typeof(TDbContext)).ToList();
            
            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }
            
            if (UseInMemoryDb)
            {
                // Регистрация SQLite InMemory
                services.AddDbContext<TDbContext>(options =>
                {
                    options.UseSqlite("DataSource=:memory:");
                });
            }
            
            // Регистрация настроек
            services.AddOptions<RabbitMqSettings>()
                .Bind(ctx.Configuration.GetSection("RabbitMq"))
                .ValidateDataAnnotations();

            // Регистрируем стратегии и генераторы
            services.AddIntegrationStrategies<TDbContext>();

            // Apply стратегии
            /*
            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();

            var strategies = scope.ServiceProvider.GetServices<IDbContextStrategy<TDbContext>>();
            foreach (var strategy in strategies)
            {
                strategy.Configure(services, ctx.Configuration);
            }
            */
            
            // Генерация данных
            /*
            var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
            var generators = scope.ServiceProvider.GetServices<IDataGenerator<TDbContext>>();

            foreach (var generator in generators)
            {
                generator.Generate(context);
            }

            context.SaveChanges();
            */
        });
    }
}