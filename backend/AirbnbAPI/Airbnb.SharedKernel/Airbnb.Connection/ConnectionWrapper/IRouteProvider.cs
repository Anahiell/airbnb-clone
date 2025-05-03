using Microsoft.Extensions.Configuration;

namespace Airbnb.Connection.ConnectionRealization;


public interface IRouteProvider
{
    string GetBaseUrlFor(string serviceName);
}

public class RouteProvider : IRouteProvider
{
    private readonly IConfiguration _config;

    public RouteProvider(IConfiguration config)
    {
        _config = config;
    }

    public string GetBaseUrlFor(string serviceName)
    {
        var transportTypeKey = "HttpConnection";
        var routesConfig = _config.GetSection($"Routes:{transportTypeKey}");

        if (routesConfig == null)
            throw new InvalidOperationException($"No routes found for transport type: {transportTypeKey}");

        var serviceRoute = routesConfig[serviceName];

        if (serviceRoute == null)
            throw new InvalidOperationException($"No route found for service '{serviceName}' under transport type '{transportTypeKey}'");

        return serviceRoute;
    }
}