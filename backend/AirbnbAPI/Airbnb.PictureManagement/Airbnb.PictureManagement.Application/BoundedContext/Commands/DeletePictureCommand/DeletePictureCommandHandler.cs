using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands.DeletePictureCommand;

public class DeletePictureCommandHandler(IRepository<DomainPicture> pictureRepository, IMediator mediator)
    : ICommandHandler<DeletePictureCommand, Result>
{
    public async Task<Result> Handle(DeletePictureCommand request, CancellationToken cancellationToken)
    {
        var picture = await pictureRepository.GetByIdAsync(request.Id, cancellationToken);
        if (picture is null)
            // return Result.Failure("Картинка не найдена");

            await pictureRepository.DeleteAsync(request.Id, cancellationToken);

        await mediator.Publish(new PictureDeletedEvent(picture.Id), cancellationToken);

        return Result.Success();
    }
}