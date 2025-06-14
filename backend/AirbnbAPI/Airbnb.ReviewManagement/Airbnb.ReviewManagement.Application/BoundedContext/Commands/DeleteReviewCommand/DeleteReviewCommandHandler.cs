using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using Airbnb.ProductManagement.Application.BoundedContext.ProductReviewUpdatedConsumer.ReviewConsumer;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Aggregates;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MediatR;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Commands.DeleteReviewCommand;

public class DeleteReviewCommandHandler(IRepository<DomainReview> reviewRepository, IReviewEventDispatcher reviewEventDispatcher, IMediator mediator)
    : ICommandHandler<DeleteReviewCommand, Result>
{
    public async Task<Result> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await reviewRepository.GetByIdAsync(request.Id, cancellationToken);
        if (review is null)
            // return Result.Failure("Отзыв не найден");

        await reviewRepository.DeleteAsync(request.Id, cancellationToken);

        await mediator.Publish(new ReviewDeletedEvent(review.Id), cancellationToken);
        
        await reviewEventDispatcher.DispatchAsync(new ProductReviewDeletedEvent()
        {
            Id = review.Id,
            ProductId = review.ProductId
        }, cancellationToken);

        return Result.Success();
    }
}