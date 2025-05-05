using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.OrderManagement.Application.BoundedContext.Services;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Aggregates;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.OrderManagement.Application.BoundedContext.Commands.CreateOrderCommand;

public class CreateOrderCommandHandler(IRepository<DomainOrder> orderRepository, IMediator mediator, ITimeZoneConverter timeZoneConverter)
    : ICommandHandler<CreateOrderCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var dateStartUtc = timeZoneConverter.ToUtc(request.DateStart, request.TimeZone);
        var dateEndUtc = timeZoneConverter.ToUtc(request.DateEnd, request.TimeZone);

        var order = new DomainOrder(request.ProductId, request.UserId, dateStartUtc, dateEndUtc);

        var result = await orderRepository.AddAsync(order, cancellationToken);

        await mediator.Publish(
            new OrderCreatedEvent(order.Id, order.ProductId, order.UserId, order.DateStart, order.DateEnd),
            cancellationToken);

        return Result<int>.Success(result);
    }
}