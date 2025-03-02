using Airbnb.Application.Messaging;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new DomainProduct(request.ProductTitle,
            request.ProductDescription,
            request.ProductPrice,
            DateTime.UtcNow,
            request.UserId,
            request.ApartmentTypeId,
            request.AddressLegalId
        );

        return await productRepository.CreateProductAsync(product, cancellationToken);
    }
}