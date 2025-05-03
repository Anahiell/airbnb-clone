using Airbnb.Connection.ConnectionService.HttpConnection.Services;

namespace Airbnb.SharedKernel.ConnectionService.HttpConnection;

/// <summary>
/// Функционал для HTTP-соединения
/// </summary>
public interface IHttpConnectionService
{
    /// <summary>
    /// Создание клиента для HTTP-подключения
    /// </summary>
    HttpClient CreateHttpClient(HttpConnectionData httpConnectionData);

    /// <summary>
    /// Выполняет GET-запрос и десериализует результат
    /// </summary>
    Task<TResponse> GetAsync<TResponse>(string route, HttpConnectionData data, object? queryParams = null);

    /// <summary>
    /// Выполняет POST-запрос с телом и десериализует ответ
    /// </summary>
    Task<TResponse> PostAsync<TRequest, TResponse>(string route, TRequest body, HttpConnectionData data);

    /// <summary>
    /// Выполняет PUT-запрос с телом и десериализует ответ
    /// </summary>
    Task<TResponse> PutAsync<TRequest, TResponse>(string route, TRequest body, HttpConnectionData data);

    /// <summary>
    /// Выполняет DELETE-запрос и десериализует результат
    /// </summary>
    Task<TResponse> DeleteAsync<TResponse>(string route, HttpConnectionData data);

    /// <summary>
    /// Универсальный метод отправки запроса с политиками и трассировкой
    /// </summary>
    Task<HttpResponseMessage> SendRequestAsync(
        HttpRequestMessage httpRequestMessage,
        HttpClient httpClient,
        CancellationToken cancellationToken,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
}