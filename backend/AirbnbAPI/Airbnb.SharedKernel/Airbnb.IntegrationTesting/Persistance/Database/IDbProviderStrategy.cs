using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.IntegrationTesting.Persistance.Database;

public interface IDbProviderStrategy
{
    void Configure(IServiceCollection services, IConfiguration configuration);
}
