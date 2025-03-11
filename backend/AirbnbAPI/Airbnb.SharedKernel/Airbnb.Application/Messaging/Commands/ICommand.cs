using Airbnb.Application.Results;
using MediatR;

namespace Airbnb.Application.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>;