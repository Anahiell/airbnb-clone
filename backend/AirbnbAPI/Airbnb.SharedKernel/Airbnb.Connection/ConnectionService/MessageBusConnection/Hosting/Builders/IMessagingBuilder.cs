using Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration.Builder;

public interface IMessagingBuilder
{
    IMessagingBuilder UseRabbitMq(Action<RabbitMqOptions> configure);
    IMessagingBuilder WithRetry(int retryCount = 3);
    IMessagingBuilder AddMiddleware<T>() where T : class, IMessageMiddleware;
    IMessagingBuilder EnableOutbox<TStorage>() where TStorage : class, IOutboxStorage;
    IServiceCollection Services { get; }
}