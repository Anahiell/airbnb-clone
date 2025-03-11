namespace Airbnb.SharedKernel.ConnectionService;

public abstract class BaseConnectionService
{
    public abstract Task<object> SendAsync(object request, CancellationToken cancellationToken);
}