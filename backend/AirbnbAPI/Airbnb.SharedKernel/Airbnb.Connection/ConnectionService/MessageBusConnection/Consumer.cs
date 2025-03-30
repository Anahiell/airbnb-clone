using MassTransit;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection;

public class Consumer : IConsumer<string>
{
    public async Task Consume(ConsumeContext<string> context)
    {
        
    }
}