using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.FileService;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.UploadUserPictureCommand;

public class UploadUserPictureCommandHandler : ICommandHandler<UploadUserPictureCommand, Result<List<int>>>
{

    private readonly IRepository<UserPicture> _userPictureRepository;
    private readonly IMediator _mediator;
    private readonly IFileService _fileService;

    public UploadUserPictureCommandHandler(IRepository<UserPicture> userPictureRepository, IMediator mediator, IFileService fileService)
    {
        _userPictureRepository = userPictureRepository;
        _mediator = mediator;
        _fileService = fileService;
    }

    public async Task<Result<List<int>>> Handle(UploadUserPictureCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null || request.UserId <= 0)
        {
            // return Result<List<string>>.Failure("Некорректный запрос");
        }

        var relativeUrl = await _fileService.SaveAsync(request.File, "User", cancellationToken);

        var picture = new UserPicture(Guid.NewGuid(), relativeUrl, request.UserId, DateTime.UtcNow);
        var createdId = await _userPictureRepository.AddAsync(picture, cancellationToken);

        await _mediator.Publish(new UserPictureCreatedEvent(picture.Id, relativeUrl, request.UserId, picture.CreatedAt), cancellationToken);

        return Result<List<int>>.Success(new List<int> { createdId });
    }
}