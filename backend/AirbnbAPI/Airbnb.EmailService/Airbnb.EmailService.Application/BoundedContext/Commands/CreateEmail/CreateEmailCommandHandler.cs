using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using MediatR;

namespace Airbnb.EmailService.Application.BoundedContext.Commands.CreateEmail;

public class CreateEmailCommandHandler : ICommandHandler<CreateEmailCommand, Result<int>>
{
    private readonly IEmailService _emailService;
    private readonly IMediator _mediator;

    public CreateEmailCommandHandler(IEmailService emailService, IMediator mediator)
    {
        _emailService = emailService;
        _mediator = mediator;
    }

    public async Task<Result<int>> Handle(CreateEmailCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body) || string.IsNullOrEmpty(request.Recipient))
        {
            return Result<int>.Failure("Недостаточно данных для отправки email");
        }

        var emailId = await _emailService.SendEmailAsync(request.Subject, request.Body, request.Recipient);

        await _mediator.Publish(new EmailSentEvent(emailId, request.Subject, request.Recipient), cancellationToken);

        return Result<int>.Success(emailId);
    }
}