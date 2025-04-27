namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration;

public class RetryPolicyOptions
{
    public int RetryCount { get; set; } = 3;
    public int InitialIntervalSeconds { get; set; } = 2;
}