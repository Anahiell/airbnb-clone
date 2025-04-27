using System.Text.Json;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Serializer;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.DeadLetterQueue;

public class DeadLetterMiddleware : IMessageMiddleware
{
    private readonly IDeadLetterQueue _dlq;

    public DeadLetterMiddleware(IDeadLetterQueue dlq)
    {
        _dlq = dlq;
    }

    public async Task InvokeAsync<T>(T message, Func<Task> next) where T : class
    {
        try
        {
            await next();
        }
        catch (Exception ex)
        {
            var serializer = message as IMessageSerializer;
            var payload = JsonSerializer.Serialize(message);
            await _dlq.HandleAsync(typeof(T).FullName!, payload, ex);
            throw;
        }
    }
}