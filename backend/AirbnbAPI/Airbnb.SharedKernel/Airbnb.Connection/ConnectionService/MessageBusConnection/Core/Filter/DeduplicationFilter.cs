namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Filter;

public class DeduplicationFilter : IMessageFilter
{
    private readonly IProcessedMessageStore _store;

    public DeduplicationFilter(IProcessedMessageStore store)
    {
        _store = store;
    }

    public async Task<bool> ShouldProcessAsync<T>(T message)
    {
        var id = message.GetType().GetProperty("Id")?.GetValue(message)?.ToString();
        if (string.IsNullOrEmpty(id)) return true;

        return !await _store.ExistsAsync(id);
    }
}
