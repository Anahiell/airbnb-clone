using Newtonsoft.Json;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Serializer;

public class NewtonsoftJsonMessageSerializer : IMessageSerializer
{
    private readonly JsonSerializerSettings _settings;

    public NewtonsoftJsonMessageSerializer(JsonSerializerSettings? settings = null)
    {
        _settings = settings ?? new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
    }

    public string Serialize<T>(T message) =>
        JsonConvert.SerializeObject(message, _settings);

    public T Deserialize<T>(string payload) =>
        JsonConvert.DeserializeObject<T>(payload, _settings)!;

    public object Deserialize(string payload, Type type) =>
        JsonConvert.DeserializeObject(payload, type, _settings)!;
}
