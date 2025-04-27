using System.Text.Json;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Serializer;

public class SystemTextJsonMessageSerializer : IMessageSerializer
{
    private readonly JsonSerializerOptions _options;

    public SystemTextJsonMessageSerializer(JsonSerializerOptions? options = null)
    {
        _options = options ?? new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public string Serialize<T>(T message) =>
        JsonSerializer.Serialize(message, _options);

    public T Deserialize<T>(string payload) =>
        JsonSerializer.Deserialize<T>(payload, _options)!;

    public object Deserialize(string payload, Type type) =>
        JsonSerializer.Deserialize(payload, type, _options)!;
}