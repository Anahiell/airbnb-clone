using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Events;
using Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Room;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.Commands.UploadProductImage;

public class UploadProductImageHandler(
    IUseCaseDispatcher useCaseDispatcher,
    IBus bus) : ICommandHandler<UploadProductImageCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
    {
        var roomResult = await useCaseDispatcher.DispatchAsync(new GetRoomIdsByPictureNamesUseCase(request.ImagesNames
            .Select(p => p)
            .ToList()), cancellationToken);

        if (!roomResult.IsSuccess)
        {
            return Result<string>.Failure(roomResult.Errors);
        }

        var roomDict = roomResult?.Value?
            .ToDictionary(x => x.Name, x => x.Id, StringComparer.OrdinalIgnoreCase);

        for (int i = 0; i < request.Images.Count; i++)
        {
            var file = request.Images[i];
            var imageName = request.ImagesNames.ElementAtOrDefault(i);


            int? roomId = null;
            if (!string.IsNullOrEmpty(imageName) && roomDict != null)
            {
                if (roomDict.TryGetValue(imageName, out var id))
                    roomId = id;
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            var pictureData = memoryStream.ToArray();

            await bus.Publish(new ProductPictureUpdatedEvent
            {
                ProductId = request.ProductId,
                PictureData = pictureData,
                RoomId = roomId,
                RoomName = imageName
            }, cancellationToken);
        }

        return Result<string>.Success("Фотографии успешно загружены.");
    }
}