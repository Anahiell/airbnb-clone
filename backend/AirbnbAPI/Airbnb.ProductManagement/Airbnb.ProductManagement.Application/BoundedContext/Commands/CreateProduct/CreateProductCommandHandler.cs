using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<CreateProductCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new DomainProduct(request.ProductTitle, request.ProductDescription, request.ProductPrice,
            DateTime.UtcNow, request.UserId, request.ApartmentTypeId, request.AddressLegalId
        );

        var result = await productRepository.CreateProductAsync(product, cancellationToken);

        return Result<int>.Success(result);
    }
}