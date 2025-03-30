using Airbnb.SharedKernel.ConnectionService.MessageBusConnection;
using MassTransit;
using MassTransit.KafkaIntegration;


// TODO Сделать нормальную реализацию для KAFKA, так как MassTransit очень плохо работает с этой очередью сообщений, 
// и с недавних версий работает с ней через Rider
/*
namespace Airbnb.Connection.ConnectionService.MessageBusConnection
{
    public class KafkaMessageBusProvider : IMessageBusProvider
    {
        private readonly IBusControl _bus;
        private readonly MessageBusOptions _options;

        public KafkaMessageBusProvider(MessageBusOptions options)
        {
            _options = options;

            _bus = Bus.Factory.CreateUsingKafka(cfg =>
            {
                cfg.Host(_options.ConnectionString);

                // Настройка конечной точки для Kafka
                cfg.ReceiveEndpoint(_options.QueueName, ep =>
                {
                    ep.Consumer<Consumer>(); // Здесь YourConsumer - это класс обработчика для Kafka
                });
            });
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        {
            // Публикация сообщения в Kafka
            await _bus.Publish(message, cancellationToken);
        }

        public async Task SubscribeAsync<T>(Func<T, Task> handler, CancellationToken cancellationToken = default)
            where T : class
        {
            // Если вам всё-таки нужно динамически подписываться, можно так:
            _bus.ConnectReceiveEndpoint("dynamic-kafka-endpoint", cfg =>
            {
                cfg.Handler<T>(async context =>
                {
                    await handler(context.Message);
                });
            });
            await _bus.StartAsync(cancellationToken);
        }
    }
}
*/