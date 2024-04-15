namespace EventHorizon.Game.Server.ServerModule.SystemLog.Show;

using EventHorizon.Observer.Model;
using MediatR;

public struct ShowMessageFromSystemEvent : INotification { }

public interface ShowMessageFromSystemEventObserver
    : ArgumentObserver<ShowMessageFromSystemEvent> { }
