using Airbnb.IntegrationTesting.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.IntegrationTesting.Persistance.Database;

public class MassTransitRabbitMqStrategy<TDbContext> : IDbContextStrategy<TDbContext>
    where TDbContext : DbContext
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        var rabbitSettings = configuration.GetSection("RabbitMq").Get<RabbitMqSettings>()
                             ?? throw new InvalidOperationException("Missing RabbitMq configuration");

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(rabbitSettings.Host, h =>
                {
                    h.Username(rabbitSettings.Username);
                    h.Password(rabbitSettings.Password);
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}