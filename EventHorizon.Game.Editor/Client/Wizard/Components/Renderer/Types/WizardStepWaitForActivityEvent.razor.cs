namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types
{
    using System.Threading.Tasks;

    using EventHorizon.Activity;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Wizard.Api;
    using EventHorizon.Zone.Systems.Wizard.Model;

    using Microsoft.AspNetCore.Components;

    public class WizardStepWaitForActivityEventBase
        : ObservableComponentBase,
        ActivityEventObserver
    {
        [CascadingParameter]
        public WizardState State { get; set; } = null!;

        [Parameter]
        public WizardStep Step { get; set; } = null!;
        [Parameter]
        public WizardData Data { get; set; } = null!;

        private string _category = string.Empty;
        private string _action = string.Empty;
        private string _tag = string.Empty;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _category = Step.Details["ActivityEvent:Category"];
            _action = Step.Details["ActivityEvent:Action"];
            _tag = Step.Details["ActivityEvent:Tag"];
        }

        public async Task Handle(
            ActivityEvent args
        )
        {
            if(args.Category == _category
                && args.Action == _action
                && args.Tag == _tag
            )
            {
                await State.Next();
            }
        }
    }
}
