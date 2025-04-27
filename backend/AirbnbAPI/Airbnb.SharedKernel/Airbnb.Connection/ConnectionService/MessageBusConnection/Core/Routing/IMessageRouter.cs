namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Routing;

public interface IMessageRouter
{
    string GetDestination<T>(T message);
}