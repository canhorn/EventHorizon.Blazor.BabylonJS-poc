namespace EventHorizon.Game.Server.Asset.Connection;

using EventHorizon.Observer.Model;

using MediatR;

public struct ConnectedToAssetServerAdmin : INotification { }

public interface ConnectedToAssetServerAdminObserver
    : ArgumentObserver<ConnectedToAssetServerAdmin> { }
