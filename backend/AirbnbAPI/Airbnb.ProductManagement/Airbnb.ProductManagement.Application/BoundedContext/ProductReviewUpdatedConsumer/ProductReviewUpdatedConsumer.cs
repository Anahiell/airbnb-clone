using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.ProductManagement.Application.BoundedContext.Events;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductReviewUpdatedConsumer;

public class ProductReviewUpdatedConsumer : IConsumer<ReviewUpdatedEvent>
{
    private readonly IRepository<DomainProduct> _productRepository;
    private readonly ILogger<ProductReviewUpdatedConsumer> _logger;
    private readonly IMediator _mediator;

    public ProductReviewUpdatedConsumer(IRepository<DomainProduct> productTagRepository, ILogger<ProductReviewUpdatedConsumer> logger, IMediator mediator)
    {
        _productRepository = productTagRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ReviewUpdatedEvent> context)
    {
        var review = context.Message;
        _logger.LogInformation("Starting to process event...");
        try
        {
            _logger.LogInformation("Processing ReviewUpdatedEvent for ProductId: {ProductId}, Rating: {Rating}", review.ProductId, review.Rating);

            var product = await _productRepository.GetByIdAsync(review.ProductId, context.CancellationToken);
            
            await _mediator.Publish(new ProductUpdatedEvent(
                product.Id,
                product.Title,
                product.Description,
                product.Price,
                product.IsAvailable,
                product.CreatedAt,
                product.UserId,
                product.ApartmentTypeId,
                product.AddressLegalId
            ));

            _logger.LogInformation("Successfully processed ProductTag with ProductId: {ProductId}, Rating: {Rating}", review.ProductId, review.Rating);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing ProductTagUpdatedEvent.");
            throw; // Повторное возбуждение исключения, если нужно будет повторить процесс.
        }
        _logger.LogInformation("Finished processing event.");
    }

}
