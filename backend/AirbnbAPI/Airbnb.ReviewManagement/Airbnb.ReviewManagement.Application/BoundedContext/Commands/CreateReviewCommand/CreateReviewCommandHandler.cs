using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Aggregates;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Commands;

public class CreateReviewCommandHandler(IRepository<DomainReview> reviewRepository, IBus bus, IMediator mediator)
    : ICommandHandler<CreateReviewCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new DomainReview(request.Comment, request.Description, request.Rating, DateTime.Now, request.UserId, request.ProductId);

        var result = await reviewRepository.AddAsync(review, cancellationToken);

        await mediator.Publish(new ReviewCreatedEvent(review.Id, review.Title, review.Description, review.Rating, review.CreatedAt, review.UserId, review.ProductId), cancellationToken);

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
        
        return Result<int>.Success(result);
    }
}