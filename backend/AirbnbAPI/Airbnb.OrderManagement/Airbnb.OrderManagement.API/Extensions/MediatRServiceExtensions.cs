using System.Reflection;
using Airbnb.Application.Behaviors;
using Airbnb.OrderManagement.Application.BoundedContext.Commands.CreateOrderCommand;
using FluentValidation;

namespace AirbnbAPI.Extensions;

public static class MediatRServiceExtensions
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateOrderCommand).GetTypeInfo().Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(QueryCachingPipelineBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(typeof(CreateOrderCommand).Assembly);

        return services;
    }
}