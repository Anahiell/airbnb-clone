using System.Text.Json;
using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries;

public interface IProductDataAggregator
{
    Task<List<ProductEntityInfo>> EnrichAsync(GetProductPaginatedQuery request, List<ProductEntityInfo> products, CancellationToken cancellationToken);
    Task EnrichAsync(List<ProductEntityInfo> products, CancellationToken cancellationToken);

}

public class ProductDataAggregator : IProductDataAggregator
{
    private readonly IHttpConnectionService  _connection;

    public ProductDataAggregator(IHttpConnectionService connection)
    {
        _connection = connection;
    }

    public async Task<List<ProductEntityInfo>> EnrichAsync(GetProductPaginatedQuery request, List<ProductEntityInfo> products, CancellationToken cancellationToken)
    {
        foreach (var product in products)
        {
            var tags = await _connection.GetAsync<List<TagInfo>>(
                "api/v1/ProductTag/GetAllProductTags",
                new HttpConnectionData { ClientName = "TagService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10, ProductId = product.Id });

            Console.WriteLine("Good Request");
            product.Tags = tags;
        }

        if (request?.Tags?.Any() == true)
        {
            var tags = new List<TagInfo>();

            foreach (var tagName in request.Tags)
            {
                var tagsByName = await _connection.GetAsync<List<TagInfo>>(
                    "api/v1/ProductTag/GetAllProductTags",
                    new HttpConnectionData { ClientName = "TagService", CancellationToken = cancellationToken },
                    new { Name = tagName, Page = 1, PageSize = 10 });

                tags.AddRange(tagsByName);
            }

            var tagNames = tags.Select(t => t.TagName).ToHashSet();

            products = products
                .Where(p => p.Tags != null && p.Tags.Any(t => tagNames.Contains(t.TagName)))
                .ToList();
        }

        foreach (var product in products)
        {
            Console.WriteLine("Good Request 2");
            var orders = await _connection.GetAsync<List<OrderInfo>>(
                "api/v1/Order/GetAllOrders",
                new HttpConnectionData { ClientName = "OrderService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10, ProductId = product.Id });

            var pictures = await _connection.GetAsync<List<PictureInfo>>(
                "api/v1/ProductPicture/GetAllProductPictures",
                new HttpConnectionData { ClientName = "PictureService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10, ProductId = product.Id });

            var reviews = await _connection.GetAsync<List<ReviewInfo>>(
                "api/v1/Review/GetAllReviews",
                new HttpConnectionData { ClientName = "ReviewService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10, ProductId = product.Id });
            
            var rating = await _connection.GetAsync<double>(
                "api/v1/Review/GetProductRating",
                new HttpConnectionData { ClientName = "ReviewService", CancellationToken = cancellationToken },
                new { ProductId = product.Id });

            product.Rating = rating;

            product.Orders = orders;
            product.Pictures = pictures;
            product.Review = reviews;
        }

        return products;
    }

        public async Task EnrichAsync(List<ProductEntityInfo> products, CancellationToken cancellationToken)
    {
        foreach (var product in products)
        {
            var orders = await _connection.GetAsync<List<OrderInfo>>(
                "api/v1/Order/GetAllOrders",
                new HttpConnectionData { ClientName = "OrderService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10, ProductId = product.Id });

            var pictures = await _connection.GetAsync<List<PictureInfo>>(
                "api/v1/Picture/GetAllPicture",
                new HttpConnectionData { ClientName = "PictureService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10 });

            var reviews = await _connection.GetAsync<List<ReviewInfo>>(
                "api/v1/Review/GetAllReviews",
                new HttpConnectionData { ClientName = "ReviewService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10, ProductId = product.Id });

            var tags = await _connection.GetAsync<List<TagInfo>>(
                "api/v1/Tag/GetAllTags",
                new HttpConnectionData { ClientName = "TagService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10 });

            product.Orders = orders;
            product.Pictures = pictures;
            product.Review = reviews;
            product.Tags = tags;
        }
    }
}