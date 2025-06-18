namespace Airbnb.Domain.BoundedContexts.ProductRoomManagement.Interfaces;

public interface IProductRoomRepository
{
    Task<Aggregates.ProductRoom?> GetByProductIdAndRoomIdAsync(int productId, int roomId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Aggregates.ProductRoom>> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Aggregates.ProductRoom>> GetByRoomIdAsync(int roomId, CancellationToken cancellationToken = default);
    
    Task<int> AddAsync(Aggregates.ProductRoom productRoom, CancellationToken cancellationToken = default);
    Task UpdateAsync(Aggregates.ProductRoom productRoom, CancellationToken cancellationToken = default);
    Task RemoveAsync(int productId, int roomId, CancellationToken cancellationToken = default);
    Task DeleteAllByProductIdAsync(int productId, CancellationToken cancellationToken = default);
}