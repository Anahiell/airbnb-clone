using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductEnricher;

public class ReviewEnricher : IProductEnricher
{
    private readonly IHttpConnectionService _connection;

    public ReviewEnricher(IHttpConnectionService connection) => _connection = connection;

    public async Task EnrichAsync(List<ProductEntityInfo> products, CancellationToken cancellationToken)
    {
        foreach (var product in products)
        {
            var reviews = await _connection.GetAsync<List<ReviewInfo>>(
                "api/v1/Review/GetAllReviews",
                new HttpConnectionData { ClientName = "ReviewService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10, ProductId = product.Id });

            product.Review = reviews ?? new List<ReviewInfo>();
        }
    }
}