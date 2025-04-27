using Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;

public interface IMessageTransportProviderFactory
{
    IMessageTransportProvider Create(MessagingOptions options);
}