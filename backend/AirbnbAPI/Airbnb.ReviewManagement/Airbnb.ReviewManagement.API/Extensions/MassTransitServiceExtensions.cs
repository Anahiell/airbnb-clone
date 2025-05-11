using System.Reflection;
using MassTransit;

namespace Airbnb.TagManagement.API.Extensions;

public static class MassTransitServiceExtensions
{
    public static IServiceCollection AddMassTransitConsumers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            // Регистрируем обработчики событий
            x.AddConsumers(Assembly.GetExecutingAssembly());

            // Настройки для консьюмера, если они есть
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMq:Username"]);
                    h.Password(configuration["RabbitMq:Password"]);
                });
                
                // Можно подключить обработку событий через консьюмеры
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddMassTransitHostedService();

        return services;
    }
}