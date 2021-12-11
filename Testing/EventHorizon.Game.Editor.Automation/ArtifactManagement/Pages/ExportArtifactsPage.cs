namespace EventHorizon.Game.Editor.Automation.ArtifactManagement.Pages;

using System;

using Atata;

using EventHorizon.Game.Editor.Automation.ArtifactManagement.Components;
using EventHorizon.Game.Editor.Automation.Layout;

using _ = ExportArtifactsPage;

[Url("/artifact/management/exports")]
public class ExportArtifactsPage
    : MainLayoutPage<_>
{
    public H1<_> Header { get; private set; }

    [FindByClass("export-artifacts__table")]
    public Table<ArtifactTableRow<_>, _> ArtifactTable
    {
        get;
        private set;
    }
}
