using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductEnricher;

public class PictureEnricher : IProductEnricher
{
    private readonly IHttpConnectionService _connection;

    public PictureEnricher(IHttpConnectionService connection) => _connection = connection;

    public async Task EnrichAsync(List<ProductEntityInfo> products, CancellationToken cancellationToken)
    {
        foreach (var product in products)
        {
            var pictures = await _connection.GetAsync<List<PictureInfo>>(
                "api/v1/ProductPicture/GetProductPictureByIdAsync",
                new HttpConnectionData { ClientName = "PictureService", CancellationToken = cancellationToken },
                new { ProductId = product.Id });

            product.Pictures = pictures ?? new List<PictureInfo>();
        }
    }
}