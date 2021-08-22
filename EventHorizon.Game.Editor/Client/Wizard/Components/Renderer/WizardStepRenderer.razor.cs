namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer
{
    using System;
    using System.Collections.Generic;

    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types;
    using EventHorizon.Zone.Systems.Wizard.Model;

    using Microsoft.AspNetCore.Components;

    public class WizardStepRendererBase
        : EditorComponentBase
    {
        [Parameter]
        public WizardStep Step { get; set; } = null!;
        [Parameter]
        public WizardData Data { get; set; } = null!;

        protected Type ComponentType { get; private set; } = typeof(WizardStepNull);
        protected IDictionary<string, object> ComponentParameters { get; } = new Dictionary<string, object>();

        private readonly IDictionary<string, Type> _stepTypes = new Dictionary<string, Type>
        {
            [WizardStepTypes.Null] = typeof(WizardStepNull),
            [WizardStepTypes.TextInput] = typeof(WizardStepTextInput),
            [WizardStepTypes.ServerProcess] = typeof(WizardStepServerProcess),
            [WizardStepTypes.Navigate] = typeof(WizardStepNavigate),
            [WizardStepTypes.FormInput] = typeof(WizardStepFormInput),
            [WizardStepTypes.CaptureCurrentLocation] = typeof(WizardStepCaptureCurrentLocation),
            [WizardStepTypes.WaitForActivityEvent] = typeof(WizardStepWaitForActivityEvent),
            [WizardStepTypes.InfoText] = typeof(WizardStepInfoText),
            [WizardStepTypes.LocationNavigate] = typeof(WizardStepLocationNavigate),
        };

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Setup();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Setup();
        }

        private void Setup()
        {

            if (!_stepTypes.TryGetValue(
                Step.Type,
                out var componentType
            ))
            {
                ComponentType = _stepTypes[WizardStepTypes.Null];
                ComponentParameters["Step"] = Step;
                ComponentParameters["Data"] = Data;
                return;
            }

            ComponentType = componentType;
            ComponentParameters["Step"] = Step;
            ComponentParameters["Data"] = Data;
        }
    }
}
