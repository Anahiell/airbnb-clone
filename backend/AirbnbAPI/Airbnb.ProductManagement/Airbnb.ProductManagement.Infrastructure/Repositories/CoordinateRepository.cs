using System.Linq.Expressions;
using Airbnb.Domain.BoundedContexts.CoordinatesManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductCoordinateManagement.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class CoordinateRepository : ICoordinateRepository
{
    private readonly AirbnbDbContext _context;

    public CoordinateRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Coordinate entity, CancellationToken cancellationToken = default)
    {
        var entry = await _context.Set<Coordinate>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }

    public async Task<Coordinate?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Coordinate>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Coordinate>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Coordinate>().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Coordinate entity, CancellationToken cancellationToken = default)
    {
        _context.Set<Coordinate>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<Coordinate>().FindAsync(new object[] { id }, cancellationToken);
        if (entity == null) return;
        _context.Set<Coordinate>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Coordinate>> GetByIdsAsync(IEnumerable<int> ids,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Coordinate>()
            .Where(f => ids.Contains(f.Id))
            .ToListAsync(cancellationToken);
    }
    
    public Task DeleteAsync(Expression<Func<Coordinate, bool>> predicate, CancellationToken cancellationToken = default)
    {
        _context.Set<Coordinate>().RemoveRange(_context.Set<Coordinate>().Where(predicate));
        return _context.SaveChangesAsync(cancellationToken);
    }
}