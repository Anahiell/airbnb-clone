using Airbnb.Application.Results;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserPictureCommand;


public class UpdateUserPictureCommandHandler : IRequestHandler<UpdateUserPictureCommand, Result<string>>
{
    public Task<Result<string>> Handle(UpdateUserPictureCommand request, CancellationToken cancellationToken)
    {

        /*
        foreach (var file in request.PictureFiles)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            var pictureData = memoryStream.ToArray();

            await bus.Publish(new ProductPictureUpdatedEvent
            {
                ProductId = product.Id,
                PictureData = pictureData
            }, cancellationToken);
        }
        */
        return Task.FromResult(Result<string>.Success("Фотография поставлена в очередь на обновление"));
    }
}