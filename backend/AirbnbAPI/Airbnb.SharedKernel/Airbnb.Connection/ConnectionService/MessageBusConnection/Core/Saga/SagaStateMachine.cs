namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public abstract class SagaStateMachine<TSaga>
    where TSaga : ISagaInstance
{
    protected List<ISagaStateStep<TSaga>> _steps = new();

    public IReadOnlyList<ISagaStateStep<TSaga>> Steps => _steps;

    protected void State(string label, Action<SagaStateBuilder<TSaga>> configure)
    {
        var builder = new SagaStateBuilder<TSaga>(label);
        configure(builder);
        _steps.Add(builder.Build());
    }

    public abstract void Configure();
}