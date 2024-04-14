namespace EventHorizon.Game.Editor.Zone.Services.Api;

using System.Threading.Tasks;

using EventHorizon.Game.Editor.Zone.Services.Model;

public interface ZoneAdminApi
{
    Task<ZoneInfo?> GetZoneInfo();
    ZoneAdminAgentApi Agent { get; }
    ZoneAdminPlayerApi Player { get; }
    ZoneAdminArtifactManagementApi ArtifactManagement { get; }
    ZoneAdminClientAssetsApi ClientAssets { get; }
    ZoneAdminClientEntityApi ClientEntity { get; }
    ZoneAdminCommandApi Command { get; }
    ZoneAdminDataStorageApi DataStorage { get; }
    ZoneAdminServerScriptsApi ServerScripts { get; }
    ZoneAdminWizardApi Wizard { get; }
}
