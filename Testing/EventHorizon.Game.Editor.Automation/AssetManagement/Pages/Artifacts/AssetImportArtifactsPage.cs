namespace EventHorizon.Game.Editor.Automation.AssetManagement.Pages.Artifacts;

using Atata;
using EventHorizon.Game.Editor.Automation.ArtifactManagement.Components;
using EventHorizon.Game.Editor.Automation.Layout;
using _ = AssetImportArtifactsPage;

[Url("/asset/artifacts/import")]
public class AssetImportArtifactsPage : MainLayoutPage<_>
{
    [FindByClass("import-artifacts__table")]
    public Table<ArtifactTableRow<_>, _> ArtifactTable { get; private set; }
}
