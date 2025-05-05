using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.ArchiveUserPictureCommand;

public class ArchiveUserPictureCommandHandler : ICommandHandler<ArchiveUserPictureCommand, Result>
{
    private readonly IRepository<UserPicture> _userPictureRepository;
    private readonly IMediator _mediator;

    public ArchiveUserPictureCommandHandler(IRepository<UserPicture> userPictureRepository, IMediator mediator)
    {
        _userPictureRepository = userPictureRepository;
        _mediator = mediator;
    }

    public async Task<Result> Handle(ArchiveUserPictureCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            // return Result.Failure("Некорректный ID");
        }

        var picture = await _userPictureRepository.GetByIdAsync(request.Id, cancellationToken);
        if (picture is null)
        {
            // return Result.Failure("Картинка не найдена");
        }

        picture.Archive();

        await _userPictureRepository.UpdateAsync(picture, cancellationToken);
        await _mediator.Publish(new UserPictureArchivedEvent(picture.Id), cancellationToken);

        return Result.Success();
    }
}