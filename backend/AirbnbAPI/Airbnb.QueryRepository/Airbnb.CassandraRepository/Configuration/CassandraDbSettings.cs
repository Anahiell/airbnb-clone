namespace Airbnb.CassandraRepository.Configuration;

public class CassandraDbSettings
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Database { get; set; }
    public string? Url { get; set; }
    public int Port { get; set; }
}