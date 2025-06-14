using System.Linq.Expressions;
using Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class AdvantageRepository : IAdvantageRepository
{
    private readonly AirbnbDbContext _context;

    public AdvantageRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Advantage entity, CancellationToken cancellationToken = default)
    {
        var entry = await _context.Set<Advantage>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }

    public async Task<Advantage?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Advantage>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Advantage>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Advantage>().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Advantage entity, CancellationToken cancellationToken = default)
    {
        _context.Set<Advantage>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<Advantage>().FindAsync(new object[] { id }, cancellationToken);
        if (entity == null) return;
        _context.Set<Advantage>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Advantage>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Advantage>()
            .Where(f => ids.Contains(f.Id))
            .ToListAsync(cancellationToken);
    }
    
    public Task DeleteAsync(Expression<Func<Advantage, bool>> predicate, CancellationToken cancellationToken = default)
    {
        _context.Set<Advantage>().RemoveRange(_context.Set<Advantage>().Where(predicate));
        return _context.SaveChangesAsync(cancellationToken);
    }
}