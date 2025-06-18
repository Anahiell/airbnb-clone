using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Domain.BoundedContexts.RoomManagement.Interfaces;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Room;

public record GetRoomIdsByPictureNamesUseCase(List<string> PictureNames)
    : IUseCase<Result<List<(int Id, string Name)>>>;

public class GetRoomIdsByPictureNamesUseCaseHandler 
    : IUseCaseHandler<GetRoomIdsByPictureNamesUseCase, Result<List<(int Id, string Name)>>>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomIdsByPictureNamesUseCaseHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result<List<(int Id, string Name)>>> Handle(GetRoomIdsByPictureNamesUseCase request, CancellationToken ct)
    {
        var distinctNames = request.PictureNames
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var rooms = new List<Domain.BoundedContexts.RoomManagement.Aggregates.Room>();
        foreach (var name in distinctNames)
        {
            var room = await _roomRepository.GetByNameAsync(name, ct);
            if (room is not null)
                rooms.Add(room);
        }

        var missing = distinctNames.Except(rooms.Select(r => r.Name), StringComparer.OrdinalIgnoreCase).ToList();
        if (missing.Count > 0)
        {
            return Result<List<(int, string)>>.Failure($"Комнаты не найдены: {string.Join(", ", missing)}");
        }

        var result = rooms.Select(r => (r.Id, r.Name)).ToList();
        return Result<List<(int, string)>>.Success(result);
    }
}