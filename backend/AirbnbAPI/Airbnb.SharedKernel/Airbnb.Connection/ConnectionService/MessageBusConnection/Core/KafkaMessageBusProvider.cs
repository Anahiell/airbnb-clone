using Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services;

  public class KafkaMessageBusProvider : IMessageTransportProvider
    {
        private readonly ILogger<KafkaMessageBusProvider> _logger;
        private readonly IProducer<string, string> _producer;
        private readonly ConsumerConfig _consumerConfig;

        public KafkaMessageBusProvider(string bootstrapServers, ILogger<KafkaMessageBusProvider> logger)
        {
            _logger = logger;

            _producer = new ProducerBuilder<string, string>(new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            }).Build();

            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = Guid.NewGuid().ToString(),
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        public async Task PublishAsync<T>(T message, string topic, string? exchange = null, CancellationToken cancellationToken = default) where T : class
        {
            var json = JsonSerializer.Serialize(message);
            await _producer.ProduceAsync(topic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = json }, cancellationToken);
            _logger.LogInformation($"Kafka: Published message to topic '{topic}'");
        }

        public Task SubscribeAsync<T>(string topic, Func<T, Task> handler, CancellationToken cancellationToken = default) where T : class
        {
            return Task.Run(() =>
            {
                using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
                consumer.Subscribe(topic);

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var cr = consumer.Consume(cancellationToken);
                        var message = JsonSerializer.Deserialize<T>(cr.Message.Value);
                        handler(message!);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Kafka: Error while consuming from topic '{topic}'");
                    }
                }
            }, cancellationToken);
        }

        public Task StartAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            _producer.Dispose();
            return Task.CompletedTask;
        }
    }