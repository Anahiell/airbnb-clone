namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Routing;

public class ConventionBasedRouter : IMessageRouter
{
    private readonly Dictionary<Type, string> _map = new();

    public ConventionBasedRouter Map<T>(string topic)
    {
        _map[typeof(T)] = topic;
        return this;
    }

    public string GetDestination<T>(T message)
    {
        return _map.TryGetValue(typeof(T), out var destination)
            ? destination
            : typeof(T).Name;
    }
}