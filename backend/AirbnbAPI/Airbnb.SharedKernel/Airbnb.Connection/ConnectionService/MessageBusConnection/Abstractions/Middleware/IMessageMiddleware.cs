namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;

public interface IMessageMiddleware
{
    Task InvokeAsync<T>(T message, Func<Task> next) where T : class;
}