namespace EventHorizon.Game.Server.Asset.Model;

using System.Collections.Generic;

public class FileManagementAssets
{
    public IEnumerable<AssetDetails> FileList { get; set; } = new List<AssetDetails>();
    public IEnumerable<AssetDetails> PathList { get; set; } = new List<AssetDetails>();
}
