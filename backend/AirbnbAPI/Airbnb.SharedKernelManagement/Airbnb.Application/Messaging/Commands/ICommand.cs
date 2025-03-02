using MediatR;

namespace Airbnb.Application.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>;
public interface ICommand : ICommand<Unit>;