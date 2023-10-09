namespace EventHorizon.Game.Editor.Automation.AssetManagement.Pages.Artifacts;

using Atata;

using EventHorizon.Game.Editor.Automation.ArtifactManagement.Components;
using EventHorizon.Game.Editor.Automation.Layout;

using _ = AssetExportArtifactsPage;

[Url("/asset/artifacts/export")]
public class AssetExportArtifactsPage : MainLayoutPage<_>
{
    [FindByClass("export-artifacts__table")]
    public Table<ArtifactTableRow<_>, _> ArtifactTable { get; private set; }
}
