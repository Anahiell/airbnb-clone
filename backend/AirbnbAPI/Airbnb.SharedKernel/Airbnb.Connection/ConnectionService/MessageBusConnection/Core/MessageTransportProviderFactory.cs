using Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services;

public class MessageTransportProviderFactory : IMessageTransportProviderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public MessageTransportProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IMessageTransportProvider Create(MessagingOptions options)
    {
        return options.TransportType switch
        {
            TransportType.Kafka =>
                new KafkaMessageBusProvider(
                    options.KafkaOptions.BootstrapServers!,
                    _serviceProvider.GetRequiredService<ILogger<KafkaMessageBusProvider>>()),

            TransportType.RabbitMq =>
                new RabbitMqMessageTransportProvider(
                    options.RabbitMqOptions,
                    _serviceProvider.GetRequiredService<ILogger<RabbitMqMessageTransportProvider>>(),
                    _serviceProvider.GetRequiredService<MiddlewarePipeline>()),

            _ => throw new NotSupportedException("Transport type not supported")
        };
    }
}
