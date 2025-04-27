namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Filter;

public interface IProcessedMessageStore
{
    Task<bool> ExistsAsync(string messageId);
    Task MarkAsProcessedAsync(string messageId);
}