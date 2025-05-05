using System.Text.Json;
using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries;

public interface IProductDataAggregator
{
    Task EnrichAsync(List<ProductEntityInfo> products, CancellationToken cancellationToken);
}

public class ProductDataAggregator : IProductDataAggregator
{
    private readonly IHttpConnectionService  _connection;

    public ProductDataAggregator(IHttpConnectionService connection)
    {
        _connection = connection;
    }

    public async Task EnrichAsync(List<ProductEntityInfo> products, CancellationToken cancellationToken)
    {
        foreach (var product in products)
        {
            var orders = await _connection.GetAsync<List<OrderInfo>>(
                "api/v1/Order/GetAllOrders",
                new HttpConnectionData { ClientName = "OrderService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10, ProductId = product.Id });

            
            /*
            var pictures = await _connection.GetAsync<List<PictureInfo>>(
                "api/v1/Picture/GetAllPicture",
                new HttpConnectionData { ClientName = "PictureService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10 });
            */
            /*
            var reviews = await _connection.GetAsync<List<ReviewInfo>>(
                "api/v1/Review/GetAllReviews",
                new HttpConnectionData { ClientName = "ReviewService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10, ProductId = product.Id });
            */
            /*
            var tags = await _connection.GetAsync<List<TagInfo>>(
                "api/v1/Tag/GetAllTags",
                new HttpConnectionData { ClientName = "TagService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10 });
            */
            product.AddOrders(orders);
            // product.AddPictures(pictures);
            // product.AddReviews(reviews);
            // product.AddTags(tags);
        }
    }
}