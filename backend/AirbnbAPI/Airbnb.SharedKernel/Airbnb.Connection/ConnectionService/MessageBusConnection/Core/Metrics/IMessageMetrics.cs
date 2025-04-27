namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Metrics;

public interface IMessageMetrics
{
    void MessageReceived(string type);
    void MessageHandled(string type, TimeSpan duration);
    void MessageFailed(string type);
}