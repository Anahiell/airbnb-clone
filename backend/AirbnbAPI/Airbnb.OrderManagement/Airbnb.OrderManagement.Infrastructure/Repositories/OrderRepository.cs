using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Aggregates;
using Airbnb.OrderManagement.Infrastructure.DataContext;
using Airbnb.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.OrderManagement.Infrastructure.Repositories;

public class OrderRepository(AirbnbOrderDbContext context) : IRepository<DomainOrder>
{
    public async Task<int> AddAsync(DomainOrder order, CancellationToken cancellationToken = default)
    {
        var entityEntry = await context.DomainOrder.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }

    public async Task<DomainOrder?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.DomainOrder.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<DomainOrder>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.DomainOrder.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(DomainOrder entity, CancellationToken cancellationToken = default)
    {
        context.DomainOrder.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await context.DomainOrder.FindAsync(id, cancellationToken);
        if (entity == null) return;
        context.DomainOrder.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}