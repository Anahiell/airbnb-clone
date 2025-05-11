using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Aggregates;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Commands.UpdateReviewCommand;

public class UpdateReviewCommandHandler(IRepository<DomainReview> reviewRepository, IBus bus, IMediator mediator)
    : ICommandHandler<UpdateReviewCommand, Result>
{
    public async Task<Result> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await reviewRepository.GetByIdAsync(request.Id, cancellationToken);
        if (review is null)
            // return Result.Failure("Отзыв не найден");

        review.UpdateReview(request.Comment, request.Description, request.Rating, DateTime.Now, review.UserId, review.ProductId);

        await reviewRepository.UpdateAsync(review, cancellationToken);

        await mediator.Publish(new ReviewUpdatedEvent(review.Id, review.Title, review.Description, review.Rating, review.CreatedAt, review.UserId, review.ProductId), cancellationToken);

        await bus.Publish(new ProductManagement.Application.BoundedContext.Events.ReviewUpdatedEvent()
        {
            ReviewId = review.Id,
            Title = review.Title,
            Description = review.Description,
            Rating = review.Rating,
            CreatedAt = review.CreatedAt,
            UserId = review.UserId,
            ProductId = review.ProductId
        }, cancellationToken);
        
        return Result.Success();
    }
}