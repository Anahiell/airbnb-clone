using Airbnb.SharedKernel.Repositories;

namespace Airbnb.Domain.BoundedContexts.RoomManagement.Interfaces;

public interface IRoomRepository : IRepository<Aggregates.Room>
{
    Task<IEnumerable<Aggregates.Room>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    Task<Aggregates.Room?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}