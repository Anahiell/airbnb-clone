using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductEnricher;

public class OrderEnricher : IProductEnricher
{
    private readonly IHttpConnectionService _connection;

    public OrderEnricher(IHttpConnectionService connection) => _connection = connection;

    public async Task EnrichAsync(List<ProductEntityInfo> products, CancellationToken cancellationToken)
    {
        foreach (var product in products)
        {
            var orders = await _connection.GetAsync<List<OrderInfo>>(
                "api/v1/Order/GetAllOrders",
                new HttpConnectionData { ClientName = "OrderService", CancellationToken = cancellationToken },
                new { Page = 1, PageSize = 10, ProductId = product.Id });

            product.Orders = orders ?? new List<OrderInfo>();
        }
    }
}