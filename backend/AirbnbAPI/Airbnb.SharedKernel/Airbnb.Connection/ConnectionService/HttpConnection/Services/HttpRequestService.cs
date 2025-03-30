using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Airbnb.SharedKernel.ConnectionService.HttpConnection.Logs.TraceIdLogic.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Extensions.Http;

namespace Airbnb.Connection.ConnectionService.HttpConnection.Services;

public enum ContentType
{
    ///
    Unknown = 0,

    ///
    ApplicationJson = 1,

    ///
    XWwwFormUrlEncoded = 2,

    ///
    Binary = 3,

    ///
    ApplicationXml = 4,

    ///
    MultipartFormData = 5,

    /// 
    TextXml = 6,

    /// 
    TextPlain = 7,

    ///
    ApplicationJwt = 8
}

public record HttpRequestData
{
    /// <summary>
    /// Тип метода
    /// </summary>
    public HttpMethod Method { get; set; }

    /// <summary>
    /// Адрес запроса
    /// </summary>\
    public Uri Uri { set; get; }

    /// <summary>
    /// Тело метода
    /// </summary>
    public object Body { get; set; }

    /// <summary>
    /// content-type, указываемый при запросе
    /// </summary>
    public ContentType ContentType { get; set; } = ContentType.ApplicationJson;

    /// <summary>
    /// Заголовки, передаваемые в запросе
    /// </summary>
    public IDictionary<string, string> HeaderDictionary { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// Коллекция параметров запроса
    /// </summary>
    public ICollection<KeyValuePair<string, string>> QueryParameterList { get; set; } =
        new List<KeyValuePair<string, string>>();
}

public record BaseHttpResponse
{
    /// <summary>
    /// Статус ответа
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Заголовки, передаваемые в ответе
    /// </summary>
    public HttpResponseHeaders Headers { get; set; }

    /// <summary>
    /// Заголовки контента
    /// </summary>
    public HttpContentHeaders ContentHeaders { get; init; }

    /// <summary>
    /// Является ли статус код успешным
    /// </summary>
    public bool IsSuccessStatusCode
    {
        get
        {
            var statusCode = (int)StatusCode;

            return statusCode >= 200 && statusCode <= 299;
        }
    }
}

// Ответ HTTP
public record HttpResponse<TResponse> : BaseHttpResponse
{
    /// <summary>
    /// Тело ответа
    /// </summary>
    public TResponse Body { get; set; }
}

/// Сервис для HTTP запросов
internal class HttpRequestService : IHttpRequestService
{
    private readonly IHttpConnectionService _httpConnectionService;
    private readonly IEnumerable<ITraceWriter> _traceWriterList;

    /// Конструктор Сервиса
    public HttpRequestService(
        IHttpConnectionService httpConnectionService,
        IEnumerable<ITraceWriter> traceWriterList)
    {
        _httpConnectionService = httpConnectionService;
        _traceWriterList = traceWriterList;
    }

    // Политика Retry
    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    // Политика Circuit Breaker
    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }

    // Политика Bulkhead
    public static IAsyncPolicy<HttpResponseMessage> GetBulkheadPolicy() =>
        Policy.BulkheadAsync<HttpResponseMessage>(100, 10);

    /// Отправить запрос
    public async Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData,
        HttpConnectionData connectionData)
    {
        var client = _httpConnectionService.CreateHttpClient(connectionData);

        var httpRequestMessage = new HttpRequestMessage();

        foreach (var traceWriter in _traceWriterList)
        {
            httpRequestMessage.Headers.Add(traceWriter.Name, traceWriter.GetValue());
        }

        var res = await _httpConnectionService.SendRequestAsync(httpRequestMessage, null, default);
        return null;
    }

    // Подготовить контент запроса
    private static HttpContent PrepairContent(object body, ContentType contentType)
    {
        switch (contentType)
        {
            case ContentType.ApplicationJson:
            {
                if (body is string stringBody)
                {
                    body = JToken.Parse(stringBody);
                }

                var serializeSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                var serializedBody = JsonConvert.SerializeObject(body, serializeSettings);
                var content = new StringContent(serializedBody, Encoding.UTF8, MediaTypeNames.Application.Json);
                return content;
            }

            case ContentType.XWwwFormUrlEncoded:
            {
                if (body is not IEnumerable<KeyValuePair<string, string>> list)
                {
                    throw new Exception(
                        $"Body for content type {contentType} must be {typeof(IEnumerable<KeyValuePair<string, string>>).Name}");
                }

                return new FormUrlEncodedContent(list);
            }
            case ContentType.ApplicationXml:
            {
                if (body is not string s)
                {
                    throw new Exception($"Body for content type {contentType} must be XML string");
                }

                return new StringContent(s, Encoding.UTF8, MediaTypeNames.Application.Xml);
            }
            case ContentType.Binary:
            {
                if (body.GetType() != typeof(byte[]))
                {
                    throw new Exception($"Body for content type {contentType} must be {typeof(byte[]).Name}");
                }

                return new ByteArrayContent((byte[])body);
            }
            case ContentType.TextXml:
            {
                if (body is not string s)
                {
                    throw new Exception($"Body for content type {contentType} must be XML string");
                }

                return new StringContent(s, Encoding.UTF8, MediaTypeNames.Text.Xml);
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null);
        }
    }
}