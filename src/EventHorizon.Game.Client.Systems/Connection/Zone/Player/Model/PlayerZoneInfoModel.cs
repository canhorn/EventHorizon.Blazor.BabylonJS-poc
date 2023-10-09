namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Model;

using System.Collections.Generic;

using EventHorizon.Game.Client.Core.I18n.Api;
using EventHorizon.Game.Client.Core.I18n.Model;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Model;
using EventHorizon.Game.Client.Engine.Particle.Api;
using EventHorizon.Game.Client.Engine.Particle.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Model;
using EventHorizon.Game.Client.Systems.ClientScripts.Api;
using EventHorizon.Game.Client.Systems.ClientScripts.Model;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
using EventHorizon.Game.Client.Systems.EntityModule;
using EventHorizon.Game.Client.Systems.EntityModule.Api;
using EventHorizon.Game.Client.Systems.Map.Api;
using EventHorizon.Game.Client.Systems.Map.Model;
using EventHorizon.Game.Client.Systems.Player.Api;
using EventHorizon.Game.Client.Systems.ServerModule;
using EventHorizon.Game.Client.Systems.ServerModule.Api;

public class PlayerZoneInfoModel : IPlayerZoneInfo
{
    public PlayerZoneDetailsModel Player { get; set; } =
        new PlayerZoneDetailsModel();
    IPlayerZoneDetails IPlayerZoneInfo.Player => Player;

    public I18nBundleModel I18nMap { get; set; } = new I18nBundleModel();
    II18nBundle IPlayerZoneInfo.I18nMap => I18nMap;

    public MapMeshDetailsModel MapMesh { get; set; } =
        new MapMeshDetailsModel();
    IMapMeshDetails IPlayerZoneInfo.MapMesh => MapMesh;

    public List<ClientAssetModel> ClientAssetList { get; set; } =
        new List<ClientAssetModel>();
    IEnumerable<ClientAsset> IPlayerZoneInfo.ClientAssetList => ClientAssetList;

    public List<GuiLayoutDataModel> GuiLayoutList { get; set; } =
        new List<GuiLayoutDataModel>();
    IEnumerable<IGuiLayoutData> IPlayerZoneInfo.GuiLayoutList => GuiLayoutList;

    public List<ObjectEntityDetailsModel> ClientEntityList { get; set; } =
        new List<ObjectEntityDetailsModel>();
    IEnumerable<IObjectEntityDetails> IPlayerZoneInfo.ClientEntityList =>
        ClientEntityList;

    public List<ObjectEntityDetailsModel> EntityList { get; set; } =
        new List<ObjectEntityDetailsModel>();
    IEnumerable<IObjectEntityDetails> IPlayerZoneInfo.EntityList => EntityList;

    public ClientScriptsAssemblyDetails ClientScriptsAssemblyDetails { get; set; } =
        new ClientScriptsAssemblyDetails();
    IClientScriptsAssemblyDetails IPlayerZoneInfo.ClientScriptsAssemblyDetails =>
        ClientScriptsAssemblyDetails;

    public List<ServerModuleScriptsModel> ServerModuleScriptList { get; set; } =
        new List<ServerModuleScriptsModel>();
    IEnumerable<IServerModuleScripts> IPlayerZoneInfo.ServerModuleScriptList =>
        ServerModuleScriptList;

    public List<ParticleTemplateModel> ParticleTemplateList { get; set; } =
        new List<ParticleTemplateModel>();
    IEnumerable<ParticleTemplate> IPlayerZoneInfo.ParticleTemplateList =>
        ParticleTemplateList;

    public List<EntityModuleScriptsModel> BaseEntityScriptModuleList { get; set; } =
        new List<EntityModuleScriptsModel>();
    IEnumerable<EntityModuleScripts> IPlayerZoneInfo.BaseEntityScriptModuleList =>
        BaseEntityScriptModuleList;
    public List<EntityModuleScriptsModel> PlayerEntityScriptModuleList { get; set; } =
        new List<EntityModuleScriptsModel>();
    IEnumerable<EntityModuleScripts> IPlayerZoneInfo.PlayerEntityScriptModuleList =>
        PlayerEntityScriptModuleList;
}
