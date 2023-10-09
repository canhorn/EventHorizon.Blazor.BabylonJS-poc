namespace EventHorizon.Game.Editor.Zone.Services.Model;

using System;
using System.Collections.Generic;

using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Model;

public class ZoneInfo
{
    public IList<ObjectEntityDetailsModel> EntityList { get; set; } =
        new List<ObjectEntityDetailsModel>().AsReadOnly();
    public IList<ObjectEntityDetailsModel> ClientEntityList { get; set; } =
        new List<ObjectEntityDetailsModel>().AsReadOnly();
    public IList<ClientAssetModel> ClientAssetList { get; set; } =
        new List<ClientAssetModel>().AsReadOnly();
}
