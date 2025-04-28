namespace Airbnb.UserManagement.Infrastructure.Configuration;

public class SqlServerSettings
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Url { get; set; }
    public int Port { get; set; }
    public string? Database { get; set; }
}