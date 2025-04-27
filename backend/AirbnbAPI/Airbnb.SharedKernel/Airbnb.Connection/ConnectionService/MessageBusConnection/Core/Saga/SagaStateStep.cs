namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public class SagaStateStep<TSaga> : ISagaStateStep<TSaga>
    where TSaga : ISagaInstance
{
    public string Label { get; }
    private readonly Func<TSaga, Task> _execute;
    private readonly Func<TSaga, Task>? _compensate;

    public SagaStateStep(
        string label,
        Func<TSaga, Task> executeAsync,
        Func<TSaga, Task>? compensateAsync = null)
    {
        Label = label;
        _execute = executeAsync;
        _compensate = compensateAsync;
    }

    public Task ExecuteAsync(TSaga saga)
        => _execute(saga);

    public Task CompensateAsync(TSaga saga)
        => _compensate?.Invoke(saga) ?? Task.CompletedTask;
}