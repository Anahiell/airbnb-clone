using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.QueryObjects;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Queries.GetRoomByIdQuery;

public class GetRoomByIdQueryHandler : IQueryHandler<GetRoomByIdQuery, Result<RoomEntityInfo>>
{
    private readonly BaseMongoRepository<RoomEntityInfo> _repository;

    public GetRoomByIdQueryHandler(BaseMongoRepository<RoomEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<RoomEntityInfo>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await _repository.FindByIdAsync(request.RoomId);
        if (room is null)
            return Result<RoomEntityInfo>.Failure("Room не найден");

        return Result<RoomEntityInfo>.Success(room);
    }
}