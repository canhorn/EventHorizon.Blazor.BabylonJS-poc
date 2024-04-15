namespace EventHorizon.Game.Client.Systems.ClientAssets.Reload;

using EventHorizon.Observer.Model;
using MediatR;

public record ReloadingClientAssetsEvent : INotification;

public interface ReloadingClientAssetsEventObserver
    : ArgumentObserver<ReloadingClientAssetsEvent> { }
