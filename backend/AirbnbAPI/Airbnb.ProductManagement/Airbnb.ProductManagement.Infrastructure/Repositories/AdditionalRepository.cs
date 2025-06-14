using System.Linq.Expressions;
using Airbnb.Domain.BoundedContexts.ProductAdditionalInfoManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class AdditionalRepository : IAdditionalRepository
{
    private readonly AirbnbDbContext _context;

    public AdditionalRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Additional entity, CancellationToken cancellationToken = default)
    {
        var entry = await _context.Set<Additional>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }

    public async Task<Additional?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Additional>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Additional>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Additional>().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Additional entity, CancellationToken cancellationToken = default)
    {
        _context.Set<Additional>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<Additional>().FindAsync(new object[] { id }, cancellationToken);
        if (entity == null) return;
        _context.Set<Additional>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public Task DeleteAsync(Expression<Func<Additional, bool>> predicate, CancellationToken cancellationToken = default)
    {
        _context.Set<Additional>().RemoveRange(_context.Set<Additional>().Where(predicate));
        return _context.SaveChangesAsync(cancellationToken);
    }
}