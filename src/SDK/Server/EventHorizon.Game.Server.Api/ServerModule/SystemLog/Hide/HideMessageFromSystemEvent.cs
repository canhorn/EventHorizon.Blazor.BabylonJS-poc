namespace EventHorizon.Game.Server.ServerModule.SystemLog.Hide;

using EventHorizon.Observer.Model;
using MediatR;

public struct HideMessageFromSystemEvent : INotification { }

public interface HideMessageFromSystemEventObserver
    : ArgumentObserver<HideMessageFromSystemEvent> { }
