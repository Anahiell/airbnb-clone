using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Events;
using Airbnb.Domain.BoundedContexts.ProductRoomManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.RoomManagement.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Room;

public record UpdateProductPicturesUseCase(int ProductId, List<string> PictureNames, List<IFormFile> PictureFiles) : IUseCase<Result<string>>;

public class UpdateProductPicturesUseCaseHandler : IUseCaseHandler<UpdateProductPicturesUseCase, Result<string>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IProductRoomRepository _productRoomRepository;
    private readonly IMediator _mediator;
    private readonly IUseCaseDispatcher _useCaseDispatcher;

    public UpdateProductPicturesUseCaseHandler(
        IMediator mediator, IProductRoomRepository productRoomRepository, IRoomRepository roomRepository, IUseCaseDispatcher useCaseDispatcher)
    {
        _mediator = mediator;
        _roomRepository = roomRepository;
        _useCaseDispatcher = useCaseDispatcher;
        _productRoomRepository = productRoomRepository;
    }

    public async Task<Result<string>> Handle(UpdateProductPicturesUseCase request, CancellationToken ct)
    {
        var pictures = request.PictureNames
            .Select((name, idx) => new ProductPicture(PictureName: name, File: request.PictureFiles.ElementAtOrDefault(idx)))
            .ToList();

        var pictureNames = pictures.Select(p => p.PictureName).ToList();

        var roomsResult = await _useCaseDispatcher.DispatchAsync(
            new GetRoomIdsByPictureNamesUseCase(pictureNames), ct);

        if (roomsResult.Errors.Count != 0)
        {
            return Result<string>.Failure(roomsResult.Errors);
        }

        var rooms = roomsResult.Value;

        await _productRoomRepository.DeleteAllByProductIdAsync(request.ProductId, ct);

        foreach (var room in rooms)
        {
            var productRoom = new ProductRoom(request.ProductId, room.Id);
            await _productRoomRepository.AddAsync(productRoom, ct);
        }

        if (rooms.Count > 0)
        {
            await _mediator.Publish(new ProductRoomsListUpdatedEvent(request.ProductId, rooms), ct);
        }

        return Result<string>.Success("Комнаты успешно обновлены");
    }
}