using MediatR;

namespace Airbnb.Application.Messaging;

internal interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull;