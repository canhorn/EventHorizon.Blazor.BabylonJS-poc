namespace EventHorizon.Game.Editor.Automation.Wizard.Pages;

using Atata;

using EventHorizon.Game.Editor.Automation.Layout;
using EventHorizon.Game.Editor.Automation.Wizard.Components;

using _ = WizardEditorPage;

[Url("/wizard-editor")]
public class WizardEditorPage : MainLayoutPage<_>
{
    public H1<_> Header { get; private set; }

    [TestSelector("wizard-editor-page__list")]
    public WizardListComponent<_> WizardList
    {
        get;
        private set;
    }

    [TestSelector("wizard-editor-page__step-editor")]
    public WizardStepEditorComponent<_> WizardStepEditor
    {
        get;
        private set;
    }
}
