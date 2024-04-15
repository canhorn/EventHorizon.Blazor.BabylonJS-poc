namespace EventHorizon.Game.Editor.Automation.Wizard.Tests;

using Atata;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Wizard.Data;
using EventHorizon.Game.Editor.Automation.Wizard.Pages;
using NUnit.Framework;
using Translations = Localization.WizardEditorPageTranslations;

public class DisplaysOnlySingleWizardInTableWhenFilteringById : WebHost
{
    [Test]
    [Category("Wizard Editor Page")]
    public void Displays_Only_Single_Wizard_In_Table_When_Filtering_By_Id()
    {
        this.Login<WizardEditorPage>()
            .Header.Should.Equal(Translations.EN_US.Header)
            .WizardList.FilterArea.Filter.Set(WizardData.MapEditor.Id)
            .WizardList.List.Rows.Count.Should.Equal(1);
    }
}
