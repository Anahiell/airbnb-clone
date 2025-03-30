using Airbnb.SharedKernel.ConnectionService.MessageBusConnection;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection;

public class MessageBusFactory
{
    public static IMessageBusProvider CreateProvider(MessageBusOptions options)
    {
        switch (options.QueueType.ToString().ToLower())
        {
            case "rabbitmq":
                return new RabbitMqMessageBusProvider(options);
            default:
                throw new NotImplementedException($"Message bus for {options.QueueType} is not supported.");
        }
    }
}