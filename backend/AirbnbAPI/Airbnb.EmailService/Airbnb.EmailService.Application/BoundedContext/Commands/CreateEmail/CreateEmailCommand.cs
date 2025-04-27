using Airbnb.Application.Messaging;
using Airbnb.Application.Results;

namespace Airbnb.EmailService.Application.BoundedContext.Commands.CreateEmail;

public class CreateEmailCommand : ICommand<Result<int>>
{
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
}