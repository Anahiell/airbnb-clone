using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.OrderManagement.Application.BoundedContext.Services;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Aggregates;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.OrderManagement.Application.BoundedContext.Commands.UpdateOrderCommand;

public class UpdateOrderCommandHandler(
    IRepository<DomainOrder> orderRepository,
    IMediator mediator,
    ITimeZoneConverter timeZoneConverter)
    : ICommandHandler<UpdateOrderCommand, Result>
{
    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (order is null)
        {
            // return Result.Failure("Заказ не найден");   
        }

        var dateStartUtc = timeZoneConverter.ToUtc(request.DateStart, request.TimeZone);
        var dateEndUtc = timeZoneConverter.ToUtc(request.DateEnd, request.TimeZone);

        order.UpdateOrder(request.ProductId, request.UserId, dateStartUtc, dateEndUtc);

        await orderRepository.UpdateAsync(order, cancellationToken);

        await mediator.Publish(new OrderUpdatedEvent(order.Id, order.ProductId, order.UserId, order.DateStart, order.DateEnd), cancellationToken);

        return Result.Success();
    }
}