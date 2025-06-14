using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain;
using Airbnb.SharedKernel.Repositories;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.ArchiveProduct;

public class ArchiveProductCommandHandler(
    IRepository<DomainProduct> productRepository
) : ICommandHandler<ArchiveProductCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ArchiveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
            // return Result<bool>.Failure("Продукт не найден");

        product.Archive();

        await productRepository.UpdateAsync(product, cancellationToken);

        return Result<bool>.Success(true);
    }
}