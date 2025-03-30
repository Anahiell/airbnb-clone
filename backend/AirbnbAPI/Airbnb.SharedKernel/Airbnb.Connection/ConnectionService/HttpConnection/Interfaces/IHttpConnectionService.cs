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
    /// <exception cref="HttpConnectionException"></exception>
    HttpClient CreateHttpClient(HttpConnectionData httpConnectionData);

    /// <summary>
    /// Отправть HTTP-запрос
    /// </summary>
    /// <exception cref="HttpConnectionException"></exception>
    Task<HttpResponseMessage> SendRequestAsync(
        HttpRequestMessage httpRequestMessage, 
        HttpClient httpClient, 
        CancellationToken cancellationToken, 
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
}