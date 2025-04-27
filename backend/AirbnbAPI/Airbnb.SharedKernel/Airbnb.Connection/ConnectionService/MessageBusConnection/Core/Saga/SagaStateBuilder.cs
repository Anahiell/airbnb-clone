namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public class SagaStateBuilder<TSaga>
    where TSaga : ISagaInstance
{
    private readonly string _label;
    private Func<TSaga, Task>? _execute;
    private Func<TSaga, Task>? _compensate;

    public SagaStateBuilder(string label) => _label = label;

    public SagaStateBuilder<TSaga> Run(Func<TSaga, Task> execute)
    {
        _execute = execute;
        return this;
    }

    public SagaStateBuilder<TSaga> OnFail(Func<TSaga, Task> compensate)
    {
        _compensate = compensate;
        return this;
    }

    public ISagaStateStep<TSaga> Build() =>
        new SagaStateStep<TSaga>(_label, _execute!, _compensate);
}