using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Aggregates;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.OrderManagement.Application.BoundedContext.Commands.DeleteOrderCommand;

public class DeleteOrderCommandHandler(IRepository<DomainOrder> orderRepository, IMediator mediator)
    : ICommandHandler<DeleteOrderCommand, Result>
{
    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (order is null)
            // return Result.Failure("Заказ не найден");

        order.DeleteOrder();
        await orderRepository.DeleteAsync(request.Id, cancellationToken);

        await mediator.Publish(new OrderDeletedEvent(order.Id), cancellationToken);

        return Result.Success();
    }
}