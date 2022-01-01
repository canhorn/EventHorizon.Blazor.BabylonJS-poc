namespace EventHorizon.Game.Editor.Automation.ZoneArtifactsManagement.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.ZoneArtifactsManagement.Localization;
using EventHorizon.Game.Editor.Automation.ZoneArtifactsManagement.Pages;

using NUnit.Framework;

using Translations = Localization.ZoneArtifactsManagementPageTranslations;

public class ValidateStructureOfZoneArtifactsManagement
    : WebHost
{
    [Test]
    [Category("Zone Artifact Management Page")]
    [Property("TestType", "Smoke")]
    public void Validate_Structure_Of_Zone_Artifacts_Management()
    {
        this.Login<ZoneArtifactsManagementPage>()
            .Header.Should.Equal(
                Translations.EN_US.Header
            )
            .Description.Should.Equal(
                Translations.EN_US.Description
            )
            .ZoneBackupArtifactsLink.Content.Should.Equal(
                Translations.EN_US.BackupArtifactsLinkText
            )
            .ZoneBackupArtifactsLink
                .ClickAndGo<ZoneServerBackupArtifactsPage>()
                .Header.Should.Be(
                    ZoneServerBackupArtifactsPageTranslations.EN_US.Header
                )
                .ArtifactTable.Should.BeVisible()
                .GoBack<ZoneArtifactsManagementPage>()
            .ZoneExportArtifactsLink.Content.Should.Equal(
                Translations.EN_US.ExportArtifactsLinkText
            )
            .ZoneExportArtifactsLink.ClickAndGo<ZoneServerExportArtifactsPage>()
                .Header.Should.Be(
                    ZoneServerExportArtifactsPageTranslations.EN_US.Header
                )
                .ArtifactTable.Should.BeVisible()
                .GoBack<ZoneArtifactsManagementPage>()
            .ZoneImportArtifactsLink.Content.Should.Equal(
                Translations.EN_US.ImportArtifactsLinkText
            )
            .ZoneImportArtifactsLink.ClickAndGo<ZoneServerImportArtifactsPage>()
                .Header.Should.Be(
                    ZoneServerImportArtifactsPageTranslations.EN_US.Header
                )
                .ArtifactTable.Should.BeVisible()
                .GoBack<ZoneArtifactsManagementPage>();
    }
}
