namespace EventHorizon.Game.Editor.Automation.AssetManagement.Pages.Artifacts;

using Atata;

using EventHorizon.Game.Editor.Automation.ArtifactManagement.Components;
using EventHorizon.Game.Editor.Automation.Components.Toolbar;
using EventHorizon.Game.Editor.Automation.Layout;

using FluentAssertions;

using _ = AssetBackupArtifactsPage;

[Url("/asset/artifacts/backup")]

public class AssetBackupArtifactsPage
    : MainLayoutPage<_>
{
    public H1<_> Header { get; private set; }

    public StandardToolbarComponent<_, StandardToolbarButtonComponent<_>> Toolbar { get; private set; }

    [FindByClass("backup-artifacts__table")]
    public ArtifactTable<_> ArtifactTable
    {
        get;
        private set;
    }



}

public class ArtifactTable<TOwner>
    : Table<ArtifactTableRow<TOwner>, TOwner>
    where TOwner : PageObject<TOwner>
{
    public TOwner GetFirstRowReferenceId(
        out string referenceId
    )
    {
        referenceId = string.Empty;
        if (Rows.Count == 0)
        {
            return Owner;
        }

        referenceId = Rows[0].ReferenceId.Value;

        return Owner;
    }
}
