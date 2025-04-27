namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Partitioning;

public interface IPartitioner
{
    int GetPartition<T>(T message, int partitionCount);
}