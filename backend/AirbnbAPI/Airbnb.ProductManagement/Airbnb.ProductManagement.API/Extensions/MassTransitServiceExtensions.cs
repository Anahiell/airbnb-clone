using System.Reflection;
using Airbnb.ProductManagement.Application.BoundedContext.Consumers;
using Airbnb.ProductManagement.Application.BoundedContext.Consumers.OrderConsumer;
using Airbnb.ProductManagement.Application.BoundedContext.Consumers.ProductPictureConsumer;
using Airbnb.ProductManagement.Application.BoundedContext.Consumers.ReviewConsumer;
using Airbnb.ProductManagement.Application.BoundedContext.Consumers.TagConsumer;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductOrder;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductTag;
using MassTransit;

namespace AirbnbAPI.Extensions;

public static class MassTransitServiceExtensions
{
    public static IServiceCollection AddMassTransitConsumers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            // Регистрируем обработчики событий
            
            // Order
            x.AddConsumer<ProductEventConsumer<ProductOrderCreatedEvent>>();
            x.AddConsumer<ProductEventConsumer<ProductOrderUpdatedEvent>>();
            x.AddConsumer<ProductEventConsumer<ProductOrderDeletedEvent>>();

            services.AddScoped<EventStrategyDispatcher>();
            services.AddScoped<IEventHandlerStrategy, ProductOrderCreatedStrategy>();
            services.AddScoped<IEventHandlerStrategy, ProductOrderUpdatedHandlerStrategy>();
            services.AddScoped<IEventHandlerStrategy, ProductOrderDeletedHandlerStrategy>();
            
            // Review
            x.AddConsumer<ProductEventConsumer<ProductReviewCreatedEvent>>();
            x.AddConsumer<ProductEventConsumer<ProductReviewUpdatedEvent>>();
            x.AddConsumer<ProductEventConsumer<ProductReviewDeletedEvent>>();

            services.AddScoped<IEventHandlerStrategy, ProductReviewCreatedHandlerStrategy>();
            services.AddScoped<IEventHandlerStrategy, ProductReviewUpdatedHandlerStrategy>();
            services.AddScoped<IEventHandlerStrategy, ProductReviewDeletedHandlerStrategy>();

            // Picture
            x.AddConsumer<ProductEventConsumer<ProductPictureCreatedEvent>>();
            x.AddConsumer<ProductEventConsumer<ProductPictureUpdatedEvent>>();
            x.AddConsumer<ProductEventConsumer<ProductPictureDeletedEvent>>();

            services.AddScoped<IEventHandlerStrategy, ProductPictureCreatedHandlerStrategy>();
            services.AddScoped<IEventHandlerStrategy, ProductPictureUpdatedHandlerStrategy>();
            services.AddScoped<IEventHandlerStrategy, ProductPictureDeletedHandlerStrategy>();

            // Tag
            x.AddConsumer<ProductEventConsumer<ProductTagCreatedEvent>>();
            x.AddConsumer<ProductEventConsumer<ProductTagUpdatedEvent>>();
            x.AddConsumer<ProductEventConsumer<ProductTagDeletedEvent>>();

            services.AddScoped<IEventHandlerStrategy, ProductTagCreatedHandlerStrategy>();
            services.AddScoped<IEventHandlerStrategy, ProductTagUpdatedHandlerStrategy>();
            services.AddScoped<IEventHandlerStrategy, ProductTagDeletedHandlerStrategy>();

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