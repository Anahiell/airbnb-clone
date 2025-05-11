using Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.ProductTagUpdatedConsumer;
using MassTransit;

namespace Airbnb.TagManagement.API.Extensions;

public static class MassTransitServiceExtensions
{
    public static IServiceCollection AddMassTransitConsumers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            // Регистрируем консьюмеров
            x.AddConsumer<ProductTagUpdatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMq:Username"]);
                    h.Password(configuration["RabbitMq:Password"]);
                });

                cfg.ReceiveEndpoint("product_tag_updated_queue", e =>
                {
                    e.Consumer<ProductTagUpdatedConsumer>(context);
                    e.Durable = true;
                    e.AutoDelete = false; 
                });
            });
        });

        services.AddMassTransitHostedService();

        return services;
    }
}