namespace EventHorizon.Game.Editor.Client.AssetManagement.Changed;

using EventHorizon.Observer.Model;
using MediatR;

public struct AssetManagementStateChangedEvent : INotification { }

public interface AssetManagementStateChangedEventObserver
    : ArgumentObserver<AssetManagementStateChangedEvent> { }
