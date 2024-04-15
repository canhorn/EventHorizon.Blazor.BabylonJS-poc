namespace EventHorizon.Game.Editor.Automation.ZoneArtifactsManagement.Pages;

using Atata;
using EventHorizon.Game.Editor.Automation.Layout;
using _ = ZoneArtifactsManagementPage;

[Url("/artifacts/zone")]
public class ZoneArtifactsManagementPage : MainLayoutPage<_>
{
    [FindByClass("page__description")]
    public Text<_> Description { get; set; }

    public Link<_> ZoneBackupArtifactsLink { get; private set; }
    public Link<_> ZoneExportArtifactsLink { get; private set; }
    public Link<_> ZoneImportArtifactsLink { get; private set; }
}
