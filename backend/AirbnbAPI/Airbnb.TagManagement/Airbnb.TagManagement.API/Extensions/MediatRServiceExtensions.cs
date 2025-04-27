using System.Reflection;
using Airbnb.Application.Behaviors;
using Airbnb.TagsManagement.Application.BoundedContext.Commands.CreateTag;
using FluentValidation;

namespace Airbnb.TagManagement.API.Extensions;

public static class MediatRServiceExtensions
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateTagCommand).GetTypeInfo().Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(QueryCachingPipelineBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(typeof(CreateTagCommand).Assembly);

        return services;
    }
}