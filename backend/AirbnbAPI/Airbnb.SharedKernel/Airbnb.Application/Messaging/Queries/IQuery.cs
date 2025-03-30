using MediatR;

namespace Airbnb.Application.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull;