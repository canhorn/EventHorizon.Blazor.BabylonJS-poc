namespace EventHorizon.Connection.Shared.Unauthorized;

using EventHorizon.Observer.Model;
using MediatR;

public struct ConnectionUnauthorizedEvent : INotification
{
    public string Identifier { get; }

    public ConnectionUnauthorizedEvent(string? identifier = null)
    {
        Identifier = identifier ?? string.Empty;
    }
}

public interface ConnectionUnauthorizedEventObserver
    : ArgumentObserver<ConnectionUnauthorizedEvent> { }
