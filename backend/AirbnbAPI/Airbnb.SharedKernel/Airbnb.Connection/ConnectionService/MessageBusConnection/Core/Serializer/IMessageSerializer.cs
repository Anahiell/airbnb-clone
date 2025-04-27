namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Serializer;

public interface IMessageSerializer
{
    string Serialize<T>(T message);
    T Deserialize<T>(string payload);
    object Deserialize(string payload, Type type);
}