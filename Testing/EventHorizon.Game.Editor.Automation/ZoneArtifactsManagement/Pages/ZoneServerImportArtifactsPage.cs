namespace EventHorizon.Game.Editor.Automation.ZoneArtifactsManagement.Pages;

using Atata;

using EventHorizon.Game.Editor.Automation.ArtifactManagement.Components;
using EventHorizon.Game.Editor.Automation.Components.Toolbar;
using EventHorizon.Game.Editor.Automation.Layout;

using _ = ZoneServerImportArtifactsPage;

[Url("/artifacts/zone/imports")]
public class ZoneServerImportArtifactsPage : MainLayoutPage<_>
{
    public StandardToolbarComponent<_, StandardToolbarButtonComponent<_>> Toolbar { get; private set; }

    [FindByClass("import-artifacts__table")]
    public ArtifactTable<_> ArtifactTable { get; private set; }
}
