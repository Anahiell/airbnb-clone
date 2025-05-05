using System.Reflection;
using Airbnb.Application.Behaviors;
using Airbnb.PictureManagement.Application.BoundedContext.Commands;
using FluentValidation;

namespace AirbnbAPI.Extensions;

public static class MediatRServiceExtensions
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(UploadProductPictureCommand).GetTypeInfo().Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(QueryCachingPipelineBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(typeof(UploadProductPictureCommand).Assembly);

        return services;
    }
}