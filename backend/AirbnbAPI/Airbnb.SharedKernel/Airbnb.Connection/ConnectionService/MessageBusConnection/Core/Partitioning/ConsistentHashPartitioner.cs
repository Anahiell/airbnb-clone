namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Partitioning;

public class ConsistentHashPartitioner : IPartitioner
{
    public int GetPartition<T>(T message, int partitionCount)
    {
        var keyProp = message.GetType().GetProperty("CorrelationId")?.GetValue(message)?.ToString();
        if (string.IsNullOrEmpty(keyProp)) return 0;
        var hash = keyProp.GetHashCode();
        return Math.Abs(hash) % partitionCount;
    }
}