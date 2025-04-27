namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.DistributedLock;

public interface IDistributedLock
{
    Task<bool> AcquireLockAsync(string key, TimeSpan timeout);
    Task ReleaseLockAsync(string key);
}