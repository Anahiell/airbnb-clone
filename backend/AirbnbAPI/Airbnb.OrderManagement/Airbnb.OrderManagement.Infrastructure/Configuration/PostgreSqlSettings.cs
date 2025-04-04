﻿namespace Airbnb.OrderManagement.Infrastructure.Configuration;

public class PostgreSqlSettings
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Url { get; set; }
    public int Port { get; set; }
    public string? Database { get; set; }
}