using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductEnricher;

public class TagEnricher : IProductEnricher
{
    private readonly IHttpConnectionService _connection;

    public TagEnricher(IHttpConnectionService connection) => _connection = connection;

    public async Task EnrichAsync(List<ProductEntityInfo> products, CancellationToken cancellationToken)
    {
        var productIds = products.Select(p => p.Id).ToList();

        var tags = await _connection.GetAsync<List<TagInfo>>(
            "api/v1/Tag/GetAllTags",
            new HttpConnectionData { ClientName = "TagService", CancellationToken = cancellationToken },
            new { ProductIds = productIds });

        var tagNames = tags.Select(t => t.TagName).ToHashSet();

        products.RemoveAll(p => p.Tags == null || !p.Tags.Any(t => tagNames.Contains(t.TagName)));
    }
}