namespace EventHorizon.Game.Editor.Automation.ArtifactManagement.Pages;

using System;

using Atata;

using EventHorizon.Game.Editor.Automation.ArtifactManagement.Components;
using EventHorizon.Game.Editor.Automation.Layout;

using _ = ImportArtifactsPage;

[Url("/artifact/management/imports")]
public class ImportArtifactsPage
    : MainLayoutPage<_>
{
    public H1<_> Header { get; private set; }

    [FindByClass("import-artifacts__table")]
    public Table<ArtifactTableRow<_>, _> ArtifactTable
    {
        get;
        private set;
    }
}
