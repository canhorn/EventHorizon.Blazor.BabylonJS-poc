namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types;

using System.Threading.Tasks;

using EventHorizon.Activity;

public class WizardStepWaitForActivityEventBase
    : WizardStepCommonBase,
        ActivityEventObserver
{
    private string _category = string.Empty;
    private string _action = string.Empty;
    private string _tag = string.Empty;

    protected override void OnInitializing()
    {
        _category = Step.Details["ActivityEvent:Category"];
        _action = Step.Details["ActivityEvent:Action"];
        _tag = Step.Details["ActivityEvent:Tag"];
    }

    public async Task Handle(ActivityEvent args)
    {
        if (
            args.Category == _category
            && args.Action == _action
            && args.Tag == _tag
        )
        {
            await State.Next();
        }
    }
}
