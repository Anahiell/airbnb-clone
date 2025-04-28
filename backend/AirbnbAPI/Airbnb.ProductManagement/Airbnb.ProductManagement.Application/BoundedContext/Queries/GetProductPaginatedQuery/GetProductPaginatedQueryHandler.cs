using System.Text.Json;
using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using MediatR;
using MongoDB.Driver;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries;

public class GetProductPaginatedQueryHandler : IQueryHandler<GetProductPaginatedQuery,
    Result<IEnumerable<ProductEntityInfo>>>
{
    private readonly BaseMongoRepository<ProductEntityInfo> _repository;
    private readonly IHttpConnectionService _httpConnectionService;

    public GetProductPaginatedQueryHandler(BaseMongoRepository<ProductEntityInfo> repository,
        IHttpConnectionService httpConnectionService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _httpConnectionService =
            httpConnectionService ?? throw new ArgumentNullException(nameof(httpConnectionService));
    }

    public async Task<Result<IEnumerable<ProductEntityInfo>>> Handle(
        GetProductPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var filter = ProductFilterBuilder.Build(request);

        var sort = request.SortOrder switch
        {
            SortState.PriceAsc => Builders<ProductEntityInfo>.Sort.Ascending(p => p.Price),
            SortState.PriceDesc => Builders<ProductEntityInfo>.Sort.Descending(p => p.Price),
            _ => Builders<ProductEntityInfo>.Sort.Descending(p => p.CreatedDate)
        };

        var result = await _repository.GetFilteredPaginatedAsync(
            filter: filter,
            sort: sort,
            page: request.Page,
            pageSize: request.PageSize
        );

        var products = result.Items.ToList();

        var reviewHttpClient = _httpConnectionService.CreateHttpClient(new HttpConnectionData { ClientName = "ReviewService" });
        var orderHttpClient = _httpConnectionService.CreateHttpClient(new HttpConnectionData { ClientName = "OrderService" });
        var pictureHttpClient = _httpConnectionService.CreateHttpClient(new HttpConnectionData { ClientName = "PictureService" });
        var tagHttpClient = _httpConnectionService.CreateHttpClient(new HttpConnectionData { ClientName = "TagService" });

        var ordersTask = GetAllOrdersAsync(orderHttpClient, cancellationToken);
        var picturesTask = GetAllPicturesAsync(pictureHttpClient, cancellationToken);
        var reviewsTask = GetAllReviewsAsync(reviewHttpClient, cancellationToken);
        var tagsTask = GetAllTagsAsync(tagHttpClient, cancellationToken);

        await Task.WhenAll(ordersTask, picturesTask, reviewsTask, tagsTask);

        var allOrders = ordersTask.Result;
        var allPictures = picturesTask.Result;
        var allReviews = reviewsTask.Result;
        var allTags = tagsTask.Result;

        foreach (var product in products)
        {
            var productOrders = allOrders.Where(o => o.ProductId == product.AddressLegalId).ToList();
            var productReviews = allReviews.Where(r => r.ProductId == product.AddressLegalId).ToList();
            var productPictures = allPictures.Where(p => p.UserId == product.UserId).ToList();
            var productTags = allTags;
            
            product.Orders = productOrders;
            product.Pictures = productPictures;
            product.Review = productReviews;
            product.Tags = productTags;
        }

        Console.WriteLine($"Items count: {products.Count()}, TotalCount: {result.TotalCount}");
        return Result<IEnumerable<ProductEntityInfo>>.Success(products);
    }

    private async Task<List<PictureInfo>> GetAllPicturesAsync(HttpClient httpClient,
        CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "GetAllPicture?Page=1&PageSize=100");
        var response = await _httpConnectionService.SendRequestAsync(request, httpClient, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return new List<PictureInfo>();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<PictureInfo>>(content) ?? new List<PictureInfo>();
    }

    private async Task<List<ReviewInfo>> GetAllReviewsAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/v1/Review/GetAllReviews?Page=1&PageSize=100");
        var response = await _httpConnectionService.SendRequestAsync(request, httpClient, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return new List<ReviewInfo>();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<ReviewInfo>>(content) ?? new List<ReviewInfo>();
    }

    private async Task<List<TagInfo>> GetAllTagsAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/v1/Tag/GetAllTags?Page=1&PageSize=100");
        var response = await _httpConnectionService.SendRequestAsync(request, httpClient, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return new List<TagInfo>();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<TagInfo>>(content) ?? new List<TagInfo>();
    }


    private async Task<List<OrderInfo>> GetAllOrdersAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/v1/Order/GetAllOrders?Page=1&PageSize=100");
        var response = await _httpConnectionService.SendRequestAsync(request, httpClient, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return new List<OrderInfo>();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<OrderInfo>>(content) ?? new List<OrderInfo>();
    }
}

public static class ProductFilterBuilder
{
    public static FilterDefinition<ProductEntityInfo> Build(GetProductPaginatedQuery request)
    {
        var builder = Builders<ProductEntityInfo>.Filter;
        var filters = new List<FilterDefinition<ProductEntityInfo>>();

        if (request.MinPrice.HasValue)
            filters.Add(builder.Gte(p => p.Price, request.MinPrice.Value));

        if (request.MaxPrice.HasValue)
            filters.Add(builder.Lte(p => p.Price, request.MaxPrice.Value));

        if (request.DateStart.HasValue)
            filters.Add(builder.Gte(p => p.CreatedDate, request.DateStart.Value));

        return filters.Any() ? builder.And(filters) : builder.Empty;
    }
}