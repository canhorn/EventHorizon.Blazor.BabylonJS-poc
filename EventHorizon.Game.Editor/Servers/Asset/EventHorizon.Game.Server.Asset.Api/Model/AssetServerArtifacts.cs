namespace EventHorizon.Game.Server.Asset.Model;

using System.Collections.Generic;

public class AssetServerArtifacts
{
    public IEnumerable<AssetServerArtifact> ExportList { get; }
    public IEnumerable<AssetServerArtifact> ImportList { get; }
    public IEnumerable<AssetServerArtifact> BackupList { get; }

    public AssetServerArtifacts(
        IEnumerable<AssetServerArtifact> exportList,
        IEnumerable<AssetServerArtifact> importList,
        IEnumerable<AssetServerArtifact> backupList
    )
    {
        ExportList = exportList;
        ImportList = importList;
        BackupList = backupList;
    }
}
