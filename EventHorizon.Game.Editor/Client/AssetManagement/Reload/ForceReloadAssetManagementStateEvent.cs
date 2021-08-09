namespace EventHorizon.Game.Editor.Client.AssetManagement.Reload
{
    using EventHorizon.Observer.Model;
    using MediatR;

    public struct ForceReloadAssetManagementStateEvent
        : INotification
    {

    }

    public interface ForceReloadAssetManagementStateEventObserver
        : ArgumentObserver<ForceReloadAssetManagementStateEvent>
    {
    }
}
