namespace EventHorizon.Game.Editor.Automation.Wizard.Tests
{
    using Atata;

    using EventHorizon.Game.Editor.Automation.Core.Browser;
    using EventHorizon.Game.Editor.Automation.IdentityServer.Data;
    using EventHorizon.Game.Editor.Automation.Wizard.Data;
    using EventHorizon.Game.Editor.Automation.Wizard.Pages;

    using Xunit;

    public class DisplaysOnlySingleWizardInTableWhenFilteringById
        : WebHost
    {
        [Trait("Category", "Wizard Editor Page")]
        [PrettyFact(
            nameof(
                DisplaysOnlySingleWizardInTableWhenFilteringById
            )
        )]
        public void Test()
        {
            this.Login<WizardEditorPage>(
                IdentityServerData.DefaultAdminUser
            )
                .Header.Should.Equal("Wizards")
                .WizardList.FilterArea.Filter.Set(
                    WizardData.MapEditor.Id
                ).WizardList.List.Rows.Count.Should.Equal(1);
        }
    }
}
