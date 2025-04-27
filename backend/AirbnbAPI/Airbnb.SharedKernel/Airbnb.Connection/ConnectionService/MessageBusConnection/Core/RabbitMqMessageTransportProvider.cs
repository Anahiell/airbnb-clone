using System.Text.Json;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services;

public class RabbitMqMessageTransportProvider : IMessageTransportProvider
{
    private readonly RabbitMqOptions _options;
    private IConnection _connection;
    private IChannel _channel;
    private readonly ILogger<RabbitMqMessageTransportProvider> _logger;
    private readonly MiddlewarePipeline _middlewarePipeline;

    public RabbitMqMessageTransportProvider(
        RabbitMqOptions options,
        ILogger<RabbitMqMessageTransportProvider> logger,
        MiddlewarePipeline middlewarePipeline)
    {
        _options = options;
        _logger = logger;
        _middlewarePipeline = middlewarePipeline;
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.Host,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password,
        };

        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(
            queue: _options.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        await _channel.CloseAsync(cancellationToken);
        await _connection.CloseAsync(cancellationToken);
    }

    public async Task PublishAsync<T>(T message, string queueName, string? exchange = null, CancellationToken cancellationToken = default) where T : class
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(message);
        await _channel.BasicPublishAsync(
            exchange: exchange ?? "",
            routingKey: queueName,
            body: body,
            cancellationToken
        );
        _logger.LogInformation("Publishing message to queue {Queue}", queueName);
    }

    public async Task SubscribeAsync<T>(string queueName, Func<T, Task> handler, CancellationToken cancellationToken = default) where T : class
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<T>(body);

                if (message != null)
                {
                    await _middlewarePipeline.ExecuteAsync(message, () => handler(message));
                }

                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                // Optionally: await _channel.BasicNackAsync(...) или Retry-политика или Nack или Dead Letter
            }
        };

        await _channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken);
        
        _logger.LogInformation("Consuming from queue {Queue}", queueName);
    }
}
