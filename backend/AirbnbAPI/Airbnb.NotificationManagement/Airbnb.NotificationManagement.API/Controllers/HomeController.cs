using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpConnectionService _connection;

    public HomeController(ILogger<HomeController> logger, IHttpConnectionService connection)
    {
        _logger = logger;
        _connection = connection;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var products = await _connection.GetAsync<List<ProductEntityInfo>>(
            "api/v1/Product/GetAllProperty",
            new HttpConnectionData { ClientName = "ProductService", CancellationToken = cancellationToken },
            new { Page = 1, PageSize = 10 });
        
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var product = await _connection.GetAsync<List<ProductEntityInfo>>(
            $"api/v1/Product/GetAllProperty",
            new HttpConnectionData { ClientName = "ProductService", CancellationToken = cancellationToken },
            new { Page = 1, PageSize = 10 });

        return View(product.FirstOrDefault());
    }
}