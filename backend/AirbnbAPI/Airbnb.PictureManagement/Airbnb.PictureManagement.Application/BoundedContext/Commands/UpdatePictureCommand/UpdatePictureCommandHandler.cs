using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands.UpdatePictureCommand;

public class UpdatePictureCommandHandler(IRepository<DomainPicture> pictureRepository, IMediator mediator)
    : ICommandHandler<UpdatePictureCommand, Result>
{
    public async Task<Result> Handle(UpdatePictureCommand request, CancellationToken cancellationToken)
    {
        var picture = await pictureRepository.GetByIdAsync(request.Id, cancellationToken);
        if (picture is null)
            // return Result.Failure("Картинка не найдена");

            picture.UpdatePicture(request.Url);

        await pictureRepository.UpdateAsync(picture, cancellationToken);

        await mediator.Publish(new PictureUpdatedEvent(picture.Id, picture.Url), cancellationToken);

        return Result.Success();
    }
}