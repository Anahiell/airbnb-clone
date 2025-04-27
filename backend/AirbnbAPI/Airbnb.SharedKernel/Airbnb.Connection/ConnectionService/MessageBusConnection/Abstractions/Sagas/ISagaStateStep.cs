namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public interface ISagaStateStep<TSaga>
    where TSaga : ISagaInstance
{
    string Label { get; }
    Task ExecuteAsync(TSaga saga);
    Task CompensateAsync(TSaga saga);
}