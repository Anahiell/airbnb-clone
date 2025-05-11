using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.ProductManagement.Application.BoundedContext.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.RegisterUserCommand;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly IBus _bus;
    public RegisterUserCommandHandler(IUserRepository userRepository, IMediator mediator, IBus bus)
    {
        _userRepository = userRepository;
        _mediator = mediator;
        _bus = bus;
    }

    public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
        {
            // return Result<int>.Failure("Пользователь с таким email уже существует.");
        }

        var hasher = new PasswordHasher<DomainUser>();

        var newUser = new DomainUser(
            fullName: request.FullName,
            email: request.Email,
            roles: request.Roles,
            dateOfBirth: request.DateOfBirth
        );
        
        var hashedPassword = hasher.HashPassword(newUser, request.Password);
        newUser.SetPassword(hashedPassword);

        var userId = await _userRepository.AddAsync(newUser, cancellationToken);
        
        await _mediator.Publish(new UserRegisterEvent(newUser.Id, newUser.FullName, newUser.Email), cancellationToken);

        using var memoryStream = new MemoryStream();
        await request.UserPicture.CopyToAsync(memoryStream, cancellationToken);
        var pictureData = memoryStream.ToArray();

        await _bus.Publish(new UserPictureUpdatedEvent
        {
            UserId = userId,
            PictureData = pictureData
        }, cancellationToken);
        
        return Result<int>.Success(userId);
    }
}