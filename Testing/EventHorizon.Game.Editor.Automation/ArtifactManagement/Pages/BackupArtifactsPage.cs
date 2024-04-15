namespace EventHorizon.Game.Editor.Automation.ArtifactManagement.Pages;

using Atata;
using EventHorizon.Game.Editor.Automation.ArtifactManagement.Components;
using EventHorizon.Game.Editor.Automation.Layout;
using _ = BackupArtifactsPage;

[Url("/artifact/management/backups")]
public class BackupArtifactsPage : MainLayoutPage<_>
{
    [FindByClass("backup-artifacts__table")]
    public Table<ArtifactTableRow<_>, _> ArtifactTable { get; private set; }
}
