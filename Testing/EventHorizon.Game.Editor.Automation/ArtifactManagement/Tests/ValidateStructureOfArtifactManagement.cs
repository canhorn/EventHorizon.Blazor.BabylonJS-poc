namespace EventHorizon.Game.Editor.Automation.ArtifactManagement.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.ArtifactManagement.Localization;
using EventHorizon.Game.Editor.Automation.ArtifactManagement.Pages;
using EventHorizon.Game.Editor.Automation.Core.Browser;

using NUnit.Framework;

using Translations = Localization.ArtifactManagementPageTranslations;

public class ValidateStructureOfArtifactManagement : WebHost
{
    [Test]
    [Category("Artifact Management Page")]
    [Property("TestType", "Smoke")]
    public void Validate_Structure_Of_Artifact_Management()
    {
        this.Login<ArtifactManagementPage>()
            .Header.Should.Equal(Translations.EN_US.Header)
            .Description.Should.Equal(Translations.EN_US.Description)
            .BackupArtifactsLink.Content.Should.Equal(
                Translations.EN_US.BackupArtifactsLinkText
            )
            .BackupArtifactsLink.ClickAndGo<BackupArtifactsPage>()
            .Header.Should.Be(BackupArtifactPageTranslations.EN_US.Header)
            .ArtifactTable.Should.BeVisible()
            .GoBack<ArtifactManagementPage>()
            .ExportArtifactsLink.Content.Should.Equal(
                Translations.EN_US.ExportArtifactsLinkText
            )
            .ExportArtifactsLink.ClickAndGo<ExportArtifactsPage>()
            .Header.Should.Be(ExportArtifactPageTranslations.EN_US.Header)
            .ArtifactTable.Should.BeVisible()
            .GoBack<ArtifactManagementPage>()
            .ImportArtifactsLink.Content.Should.Equal(
                Translations.EN_US.ImportArtifactsLinkText
            )
            .ImportArtifactsLink.ClickAndGo<ImportArtifactsPage>()
            .Header.Should.Be(ImportArtifactPageTranslations.EN_US.Header)
            .ArtifactTable.Should.BeVisible()
            .GoBack<ArtifactManagementPage>();
    }
}
