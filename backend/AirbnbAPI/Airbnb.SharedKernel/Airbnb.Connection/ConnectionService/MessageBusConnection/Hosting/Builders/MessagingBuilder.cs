using Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Services;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration.Builder;

public class MessagingBuilder : IMessagingBuilder
{
    public IServiceCollection Services { get; }

    private readonly List<Func<IMessageMiddleware>> _middlewareFactories = [];

    public MessagingBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IMessagingBuilder UseRabbitMq(Action<RabbitMqOptions> configure)
    {
        Services.Configure(configure);
        Services.AddSingleton<IMessageTransportProvider, RabbitMqMessageTransportProvider>();
        return this;
    }

    public IMessagingBuilder WithRetry(int retryCount = 3)
    {
        Services.AddSingleton(sp =>
            new Func<IMessageMiddleware>(() =>
                new RetryMiddleware(sp.GetRequiredService<ILogger<RetryMiddleware>>(), retryCount))
        );
        return this;
    }

    public IMessagingBuilder AddMiddleware<T>() where T : class, IMessageMiddleware
    {
        Services.AddSingleton<Func<IMessageMiddleware>>(sp =>
            () => ActivatorUtilities.CreateInstance<T>(sp));
        return this;
    }

    public IMessagingBuilder EnableOutbox<TStorage>() where TStorage : class, IOutboxStorage
    {
        Services.AddSingleton<IOutboxStorage, TStorage>();
        Services.AddHostedService<OutboxDispatcher>();
        return this;
    }
}