using Airbnb.Application.Results;
using MediatR;

namespace Airbnb.Application.Messaging;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>;