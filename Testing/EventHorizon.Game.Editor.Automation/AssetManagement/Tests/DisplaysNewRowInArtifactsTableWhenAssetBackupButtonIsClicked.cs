namespace EventHorizon.Game.Editor.Automation.AssetManagement.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.ArtifactManagement.Tests;
using EventHorizon.Game.Editor.Automation.AssetManagement.Pages.Artifacts;
using EventHorizon.Game.Editor.Automation.Core.Browser;

using Xunit;

using Translations = Localization.Artifacts.AssetBackupArtifactsPageTranslations;

public class DisplaysNewRowInArtifactsTableWhenAssetBackupButtonIsClicked
    : WebHost
{
    [Trait("Category", "Asset Backup Artifacts Page")]
    [PrettyFact(
        nameof(
            DisplaysNewRowInArtifactsTableWhenAssetBackupButtonIsClicked
        )
    )]
    public void Test()
    {
        this.Login<AssetBackupArtifactsPage>()
            .Header.Should.Equal(
                Translations.EN_US.Header
            )
            .ArtifactTable.GetFirstRowReferenceId(
                out var referenceId
            )
            .Toolbar.Children[
                a => a.Content == "Assets Backup"
            ].Click()
            .ArtifactTable
            .Rows[0]
                .ReferenceId.Should.Not.Equal(
                    referenceId
                );
    }
}
