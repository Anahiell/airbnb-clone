using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;


public class ProductRoomRepository : IProductRoomRepository
{
    private readonly AirbnbDbContext _context;

    public ProductRoomRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<ProductRoom?> GetByProductIdAndRoomIdAsync(int productId, int roomId, CancellationToken cancellationToken = default)
    {
        return await _context.ProductRooms
            .FirstOrDefaultAsync(x => x.ProductId == productId && x.RoomId == roomId, cancellationToken);
    }

    public async Task<IEnumerable<ProductRoom>> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await _context.ProductRooms
            .Where(x => x.ProductId == productId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductRoom>> GetByRoomIdAsync(int roomId, CancellationToken cancellationToken = default)
    {
        return await _context.ProductRooms
            .Where(x => x.RoomId == roomId)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(ProductRoom productRoom, CancellationToken cancellationToken = default)
    {
        var entry = await _context.ProductRooms.AddAsync(productRoom, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }

    public async Task UpdateAsync(ProductRoom productRoom, CancellationToken cancellationToken = default)
    {
        _context.ProductRooms.Update(productRoom);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(int productId, int roomId, CancellationToken cancellationToken = default)
    {
        var entity = await GetByProductIdAndRoomIdAsync(productId, roomId, cancellationToken);
        if (entity != null)
        {
            _context.ProductRooms.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteAllByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        var entities = await _context.ProductRooms
            .Where(x => x.ProductId == productId)
            .ToListAsync(cancellationToken);

        if (entities.Count > 0)
        {
            _context.ProductRooms.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}