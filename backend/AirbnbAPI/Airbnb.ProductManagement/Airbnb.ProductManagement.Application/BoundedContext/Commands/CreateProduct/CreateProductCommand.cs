using Airbnb.Application.Messaging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;

public record CreateProductCommand(
    string ProductTitle,
    string ProductDescription,
    int ProductPrice,
    int UserId,
    int ApartmentTypeId,
    int AddressLegalId) : ICommand<int>;