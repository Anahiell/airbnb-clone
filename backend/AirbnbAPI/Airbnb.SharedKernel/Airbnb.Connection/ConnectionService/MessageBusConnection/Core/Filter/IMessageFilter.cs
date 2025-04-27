namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Filter;

public interface IMessageFilter
{
    Task<bool> ShouldProcessAsync<T>(T message);
}