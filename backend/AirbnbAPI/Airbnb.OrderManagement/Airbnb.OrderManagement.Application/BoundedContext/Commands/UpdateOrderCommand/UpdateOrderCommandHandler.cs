using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Aggregates;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.OrderManagement.Application.BoundedContext.Commands.UpdateOrderCommand;

public class UpdateOrderCommandHandler(IRepository<DomainOrder> orderRepository, IMediator mediator)
    : ICommandHandler<UpdateOrderCommand, Result>
{
    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (order is null)
            // return Result.Failure("Заказ не найден");

        order.UpdateOrder(request.ProductId, request.UserId, request.DateStart, request.DateEnd);

        await orderRepository.UpdateAsync(order, cancellationToken);

        await mediator.Publish(new OrderUpdatedEvent(order.Id, order.ProductId, order.UserId, order.DateStart, order.DateEnd), cancellationToken);

        return Result.Success();
    }
}