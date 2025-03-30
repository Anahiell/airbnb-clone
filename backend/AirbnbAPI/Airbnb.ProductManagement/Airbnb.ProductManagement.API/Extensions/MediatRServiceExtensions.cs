using System.Reflection;
using Airbnb.Application.Behaviors;
using Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;
using FluentValidation;

namespace AirbnbAPI.Extensions;

public static class MediatRServiceExtensions
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateProductCommand).GetTypeInfo().Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(QueryCachingPipelineBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(typeof(CreateProductCommand).Assembly);

        return services;
    }
}