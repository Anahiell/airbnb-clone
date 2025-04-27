using System.Reflection;
using Airbnb.Application.Behaviors;
using Airbnb.ReviewManagement.Application.BoundedContext.Commands;
using FluentValidation;

namespace Airbnb.TagManagement.API.Extensions;

public static class MediatRServiceExtensions
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateReviewCommand).GetTypeInfo().Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(QueryCachingPipelineBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(typeof(CreateReviewCommand).Assembly);

        return services;
    }
}