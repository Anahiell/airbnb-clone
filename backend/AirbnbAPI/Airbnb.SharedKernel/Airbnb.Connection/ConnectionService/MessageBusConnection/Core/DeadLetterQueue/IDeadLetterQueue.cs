namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.DeadLetterQueue;

public interface IDeadLetterQueue
{
    Task HandleAsync(string messageType, string payload, Exception exception);
}