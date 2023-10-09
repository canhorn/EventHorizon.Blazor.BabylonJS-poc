namespace EventHorizon.Game.Editor.Automation.AssetManagement.Pages.Artifacts;

using Atata;

using EventHorizon.Game.Editor.Automation.ArtifactManagement.Components;
using EventHorizon.Game.Editor.Automation.Components.Toolbar;
using EventHorizon.Game.Editor.Automation.Layout;

using _ = AssetBackupArtifactsPage;

[Url("/asset/artifacts/backup")]
public class AssetBackupArtifactsPage : MainLayoutPage<_>
{
    public StandardToolbarComponent<
        _,
        StandardToolbarButtonComponent<_>
    > Toolbar { get; private set; }

    [FindByClass("backup-artifacts__table")]
    public ArtifactTable<_> ArtifactTable { get; private set; }
}
