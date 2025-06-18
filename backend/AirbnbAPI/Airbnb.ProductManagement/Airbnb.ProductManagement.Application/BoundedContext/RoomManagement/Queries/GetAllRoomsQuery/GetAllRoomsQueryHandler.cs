using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.QueryObjects;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Queries.GetAllRoomsQuery;

public class GetAllRoomsQueryHandler : IQueryHandler<GetAllRoomsQuery, Result<List<RoomEntityInfo>>>
{
    private readonly BaseMongoRepository<RoomEntityInfo> _repository;

    public GetAllRoomsQueryHandler(BaseMongoRepository<RoomEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<List<RoomEntityInfo>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _repository.GetAllAsync();
        return Result<List<RoomEntityInfo>>.Success(rooms.ToList());
    }
}