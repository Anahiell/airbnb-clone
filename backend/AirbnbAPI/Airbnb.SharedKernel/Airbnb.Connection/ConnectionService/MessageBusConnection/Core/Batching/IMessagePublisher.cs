namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Batching;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message) where T : class;
}