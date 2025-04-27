namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Schedulers;

public interface IMessageScheduler
{
    Task ScheduleAsync<T>(T message, DateTimeOffset executeAt) where T : class;
}