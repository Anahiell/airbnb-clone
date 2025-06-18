using Airbnb.Domain.BoundedContexts.RoomManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.RoomManagement.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AirbnbDbContext _context;

    public RoomRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Room entity, CancellationToken cancellationToken = default)
    {
        var entry = await _context.Set<Room>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }

    public async Task<Room?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Room>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Room>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Room>().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Room entity, CancellationToken cancellationToken = default)
    {
        _context.Set<Room>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<Room>().FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _context.Set<Room>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Room>()
            .Where(r => ids.Contains(r.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<Room?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Room>()
            .FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
    }
}