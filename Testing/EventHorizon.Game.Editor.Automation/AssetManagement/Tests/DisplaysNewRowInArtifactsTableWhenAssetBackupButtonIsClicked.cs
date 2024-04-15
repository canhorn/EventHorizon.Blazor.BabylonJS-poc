namespace EventHorizon.Game.Editor.Automation.AssetManagement.Tests;

using Atata;
using EventHorizon.Game.Editor.Automation.AssetManagement.Pages.Artifacts;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using NUnit.Framework;
using Translations = Localization.Artifacts.AssetBackupArtifactsPageTranslations;

public class DisplaysNewRowInArtifactsTableWhenAssetBackupButtonIsClicked : WebHost
{
    private const int Seconds_To_Wait_For_Backup_Creation = 30;

    [Test]
    [Category("Asset Backup Artifacts Page")]
    public void Displays_New_Row_In_Artifacts_Table_When_Asset_Backup_Button_Is_Clicked()
    {
        this.Login<AssetBackupArtifactsPage>()
            .Header.Should.Equal(Translations.EN_US.Header)
            .ArtifactTable.GetFirstRowReferenceId(out var referenceId)
            .Toolbar.Children[a => a.Content == Translations.EN_US.ToolbarBackupButton]
            .Click()
            .ArtifactTable.Rows[0]
            .ReferenceId.Should.Within(Seconds_To_Wait_For_Backup_Creation)
            .Not.Equal(referenceId);
    }
}
