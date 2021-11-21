namespace EventHorizon.Game.Server.Asset.Finished;

using EventHorizon.Observer.Model;

using MediatR;

public struct AssetServerBackupUploadedEvent : INotification
{
    public string Service { get; }
    public string BackupPath { get; }

    public AssetServerBackupUploadedEvent(
        string referenceId,
        string importPath
    )
    {
        Service = referenceId;
        BackupPath = importPath;
    }
}

public interface AssetServerBackupUploadedEventObserver
    : ArgumentObserver<AssetServerBackupUploadedEvent>
{
}
