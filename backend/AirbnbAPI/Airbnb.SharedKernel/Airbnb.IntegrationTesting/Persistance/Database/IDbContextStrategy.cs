using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.IntegrationTesting.Persistance.Database;

public interface IDbContextStrategy<TDbContext> : IDbProviderStrategy
    where TDbContext : DbContext;