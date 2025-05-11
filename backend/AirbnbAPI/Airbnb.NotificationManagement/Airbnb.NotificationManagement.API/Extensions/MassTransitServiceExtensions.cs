using MassTransit;
using WebApplication1.Consumers;

namespace WebApplication1.Extensions;

public static class MassTransitServiceExtensions
{
    public static IServiceCollection AddMassTransitConsumers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            // Регистрируем обработчики событий
            x.AddConsumer<ProductCreatedConsumer>();
            x.AddConsumer<ProductUpdatedConsumer>();

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