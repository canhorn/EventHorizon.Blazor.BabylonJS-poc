namespace EventHorizon.Game.Server.Asset.Model;

using System.Collections.Generic;

public class ArtifactListResult
{
    public List<AssetServerArtifact> ExportList { get; set; } = new List<AssetServerArtifact>();
    public List<AssetServerArtifact> ImportList { get; set; } = new List<AssetServerArtifact>();
    public List<AssetServerArtifact> BackupList { get; set; } = new List<AssetServerArtifact>();
}
