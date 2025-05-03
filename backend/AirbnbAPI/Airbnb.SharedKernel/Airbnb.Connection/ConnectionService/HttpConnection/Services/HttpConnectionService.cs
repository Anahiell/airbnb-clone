using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Airbnb.Connection.ConnectionRealization;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Airbnb.SharedKernel.ConnectionService.HttpConnection.Logs.TraceIdLogic.Interfaces;
using Polly;
using Polly.Extensions.Http;

namespace Airbnb.Connection.ConnectionService.HttpConnection.Services;

public record struct HttpConnectionData()
{
    public TimeSpan? Timeout { get; set; } = null;
    public CancellationToken CancellationToken { get; set; } = default;
    public string ClientName { get; set; }
}

public class HttpConnectionService : IHttpConnectionService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IRouteProvider _routeProvider;
    private readonly IEnumerable<ITraceWriter> _traceWriters;

    public HttpConnectionService(
        IHttpClientFactory httpClientFactory,
        IRouteProvider routeProvider,
        IEnumerable<ITraceWriter> traceWriters)
    {
        _httpClientFactory = httpClientFactory;
        _routeProvider = routeProvider;
        _traceWriters = traceWriters;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

    private static IAsyncPolicy<HttpResponseMessage> GetBulkheadPolicy() =>
        Policy.BulkheadAsync<HttpResponseMessage>(100, 10);

    private static IAsyncPolicy<HttpResponseMessage> GetPolicy() =>
        Policy.WrapAsync(GetBulkheadPolicy(), GetRetryPolicy(), GetCircuitBreakerPolicy());

    public HttpClient CreateHttpClient(HttpConnectionData data)
    {
        var httpClient = string.IsNullOrWhiteSpace(data.ClientName)
            ? _httpClientFactory.CreateClient()
            : _httpClientFactory.CreateClient(data.ClientName);

        if (data.Timeout.HasValue)
        {
            httpClient.Timeout = data.Timeout.Value;
        }

        return httpClient;
    }

    public async Task<T> GetAsync<T>(string route, HttpConnectionData data, object? queryParams = null)
    {
        if (queryParams is not null)
        {
            route = QueryStringHelper.AddQueryStringFromObject(route, queryParams);
        }

        var client = CreateHttpClient(data);
        var uri = BuildUri(route, data);
        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        AddTraceHeaders(request);

        var response = await SendRequestAsync(request, client, data.CancellationToken);
        return await DeserializeAsync<T>(response);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string route, TRequest body, HttpConnectionData data)
    {
        var client = CreateHttpClient(data);
        var uri = BuildUri(route, data);

        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = content
        };

        AddTraceHeaders(request);

        var response = await SendRequestAsync(request, client, data.CancellationToken);
        return await DeserializeAsync<TResponse>(response);
    }

    public async Task<TResponse> PutAsync<TRequest, TResponse>(string route, TRequest body, HttpConnectionData data)
    {
        var client = CreateHttpClient(data);
        var uri = BuildUri(route, data);

        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Put, uri)
        {
            Content = content
        };

        AddTraceHeaders(request);

        var response = await SendRequestAsync(request, client, data.CancellationToken);
        return await DeserializeAsync<TResponse>(response);
    }

    public async Task<T> DeleteAsync<T>(string route, HttpConnectionData data)
    {
        var client = CreateHttpClient(data);
        var uri = BuildUri(route, data);

        var request = new HttpRequestMessage(HttpMethod.Delete, uri);

        AddTraceHeaders(request);

        var response = await SendRequestAsync(request, client, data.CancellationToken);
        return await DeserializeAsync<T>(response);
    }

    public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, HttpClient client,
        CancellationToken cancellationToken,
        HttpCompletionOption option = HttpCompletionOption.ResponseContentRead)
    {
        var policy = GetPolicy();
        return await policy.ExecuteAsync(() =>
            client.SendAsync(request, option, cancellationToken));
    }

    private Uri BuildUri(string route, HttpConnectionData data)
    {
        var baseUrl = _routeProvider.GetBaseUrlFor(data.ClientName);
        return new Uri(new Uri(baseUrl), route);
    }

    private void AddTraceHeaders(HttpRequestMessage request)
    {
        foreach (var traceWriter in _traceWriters)
        {
            request.Headers.TryAddWithoutValidation(traceWriter.Name, traceWriter.GetValue());
        }
    }

    private static async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Ошибка запроса: {response.StatusCode}, {error}");
        }

        var stream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<T>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("Не удалось десериализовать ответ.");
    }
}

public static class QueryStringHelper
{
    public static string AddQueryStringFromObject(string basePath, object parameters)
    {
        var props = parameters
            .GetType()
            .GetProperties()
            .Where(p => p.GetIndexParameters().Length == 0)
            .Where(p => p.CanRead && p.GetMethod?.GetParameters().Length == 0)
            .ToDictionary(p => p.Name, p => p.GetValue(parameters)?.ToString());

        if (!props.Any()) return basePath;

        var query = string.Join("&", props
            .Where(kv => kv.Value != null)
            .Select(kv => $"{WebUtility.UrlEncode(kv.Key)}={WebUtility.UrlEncode(kv.Value)}"));

        var separator = basePath.Contains('?') ? "&" : "?";
        return basePath + separator + query;
    }
}