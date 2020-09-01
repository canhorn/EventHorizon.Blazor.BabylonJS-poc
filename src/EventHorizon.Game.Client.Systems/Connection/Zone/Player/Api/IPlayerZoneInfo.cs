﻿namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientScripts.Api;
    using EventHorizon.Game.Client.Systems.Map.Api;
    using EventHorizon.Game.Client.Systems.ServerModule.Api;

    public interface IPlayerZoneInfo
    {
        IPlayerZoneDetails Player { get; }
        IMapMeshDetails MapMesh { get; }
        IEnumerable<IClientAsset> ClientAssetList { get; }
        IEnumerable<IGuiLayoutData> GuiLayoutList { get; }
        IEnumerable<IObjectEntityDetails> ClientEntityList { get; }
        IEnumerable<IObjectEntityDetails> EntityList { get; }
        IClientScriptsAssemblyDetails ClientScriptsAssemblyDetails { get; }
        IEnumerable<IServerModuleScripts> ServerModuleScriptList { get; }
    }
}