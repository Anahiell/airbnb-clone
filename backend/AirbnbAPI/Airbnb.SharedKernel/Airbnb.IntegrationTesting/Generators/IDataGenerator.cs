using Microsoft.EntityFrameworkCore;

namespace Airbnb.IntegrationTesting.Generators;

public interface IDataGenerator<TDbContext>
    where TDbContext : DbContext
{
    void Generate(TDbContext context);
}