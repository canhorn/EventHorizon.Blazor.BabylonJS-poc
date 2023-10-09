namespace EventHorizon.Game.Editor.Automation.ZoneArtifactsManagement.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.ZoneArtifactsManagement.Pages;

using NUnit.Framework;

using Translations = Localization.ZoneServerBackupArtifactsPageTranslations;

public class DisplayNewZoneArtifactBackupInTableWhenZoneBackupIsTriggered
    : WebHost
{
    private const int Seconds_To_Wait_For_Backup_Creation = 30;

    [Test]
    [Category("Zone Artifact Management Page")]
    public void Display_New_Zone_Artifact_Backup_In_Table_When_Zone_Backup_Is_Triggered()
    {
        this.Login<ZoneServerBackupArtifactsPage>()
            .ArtifactTable.GetFirstRowReferenceId(out var referenceId)
            .Toolbar.Children[
            a => a.Content == Translations.EN_US.ToolbarZoneBackupButton
        ]
            .Click()
            .ArtifactTable.Rows[0].ReferenceId.Should
            .Within(Seconds_To_Wait_For_Backup_Creation)
            .Not.Equal(referenceId);
    }
}
