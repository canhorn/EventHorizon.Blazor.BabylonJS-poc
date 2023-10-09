namespace EventHorizon.Game.Server.Asset.Finished;

using EventHorizon.Observer.Model;

using MediatR;

public struct AssetServerBackupFinishedEvent : INotification
{
    public string ReferenceId { get; }
    public string BackupPath { get; }

    public AssetServerBackupFinishedEvent(string referenceId, string backupPath)
    {
        ReferenceId = referenceId;
        BackupPath = backupPath;
    }
}

public interface AssetServerBackupFinishedEventObserver
    : ArgumentObserver<AssetServerBackupFinishedEvent> { }
