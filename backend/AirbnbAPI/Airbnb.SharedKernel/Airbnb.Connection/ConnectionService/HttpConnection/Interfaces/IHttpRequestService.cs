using Airbnb.Connection.ConnectionService.HttpConnection.Services;

namespace Airbnb.SharedKernel.ConnectionService.HttpConnection;

public interface IHttpRequestService
{
    /// <summary>
    /// Отправить HTTP-запрос
    /// </summary>
    Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData, HttpConnectionData connectionData = default);
}