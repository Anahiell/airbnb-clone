

using System.Collections;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Airbnb.Connection.ConnectionRealization;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Airbnb.SharedKernel.ConnectionService.HttpConnection.Logs.TraceIdLogic.Interfaces;
using Microsoft.AspNetCore.Http;
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

    public async Task<TResponse> PostAsync<TRequest, TResponse>(
        string route,
        HttpConnectionData data,
        TRequest? body = default,
        object? query = null,
        bool serializeEnumsAsStrings = false)
    {
        if (query is not null)
        {
            route = QueryStringHelper.AddQueryStringFromObject(route, query);
        }

        var client = CreateHttpClient(data);
        var uri = BuildUri(route, data);
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        AddTraceHeaders(request);

        if (body is not null)
        {
            var type = typeof(TRequest);
            var fileProps = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => typeof(IFormFile).IsAssignableFrom(p.PropertyType))
                .ToList();

            if (fileProps.Count != 0)
            {
                var content = new MultipartFormDataContent();

                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var value = prop.GetValue(body);

                    if (value is IFormFile file)
                    {
                        var fileContent = new StreamContent(file.OpenReadStream());
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType ?? "application/octet-stream");
                        content.Add(fileContent, prop.Name, file.FileName);
                    }
                    else if (value != null)
                    {
                        var stringValue = value.ToString();
                        if (value is Enum || serializeEnumsAsStrings)
                            stringValue = JsonSerializer.Serialize(value).Trim('"');

                        content.Add(new StringContent(stringValue), prop.Name);
                    }
                }

                request.Content = content;
            }
            else
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                if (serializeEnumsAsStrings)
                {
                    options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                }

                var json = JsonSerializer.Serialize(body, options);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
        }

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
            .Where(p => p.CanRead && p.GetMethod?.GetParameters().Length == 0)
            .Where(p => 
            {
                var val = p.GetValue(parameters);
                if (val == null) return false;
                var type = val.GetType();

                if (typeof(Microsoft.AspNetCore.Http.IFormFile).IsAssignableFrom(type))
                    return false;

                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(type) && type != typeof(string))
                {
                    var elemType = type.IsGenericType ? type.GetGenericArguments()[0] : null;
                    if (elemType != null && typeof(Microsoft.AspNetCore.Http.IFormFile).IsAssignableFrom(elemType))
                        return false;
                }
                return true;
            })
            .ToDictionary(p => p.Name, p => p.GetValue(parameters)?.ToString());

        if (!props.Any()) return basePath;

        var query = string.Join("&", props
            .Where(kv => kv.Value != null)
            .Select(kv => $"{WebUtility.UrlEncode(kv.Key)}={WebUtility.UrlEncode(kv.Value)}"));

        var separator = basePath.Contains('?') ? "&" : "?";
        return basePath + separator + query;
    }
    
    public static string AddQueryStringFromDictionary(string uri, Dictionary<string, object?> values)
    {
        if (values == null || !values.Any())
            return uri;

        var query = string.Join("&", values
            .Where(kv => kv.Value != null)
            .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!.ToString()!)}"));

        var separator = uri.Contains("?") ? "&" : "?";
        return uri + separator + query;
    }
}