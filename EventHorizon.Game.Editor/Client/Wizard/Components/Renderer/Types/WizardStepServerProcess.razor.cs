namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types;

using System;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Wizard.Invalidate;
using EventHorizon.Game.Editor.Client.Wizard.Processing;
using EventHorizon.Game.Editor.Client.Wizard.Update;
using EventHorizon.Zone.Systems.Wizard.Run;

public class WizardStepServerProcessBase : WizardStepCommonBase
{
    public string SuccessMessage { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;

    protected override async Task OnInitializingAsync()
    {
        if (Data[$"Processor:{Step.Id}:Ran"] == "true")
        {
            SuccessMessage = Localizer[
                "Successful Processed Request, please continue to next step."
            ];
            return;
        }

        await Mediator.Send(new SetProcessingOnWizardCommand(ContextState.Context, true));

        var result = await Mediator.Send(
            new RunWizardScriptProcessorCommand(
                State.CurrentWizardId(ContextState.Context),
                Step.Id,
                Step.Details["Processor:ScriptId"],
                Data
            )
        );

        if (!result)
        {
            ErrorMessage = Localizer[result.ErrorCode];
            if (ErrorMessage == result.ErrorCode)
            {
                // since they match show a standard Error Code Message
                ErrorMessage = Localizer["Error Code: {0}", result.ErrorCode];
            }

            await Mediator.Send(
                new SetWizardToInvalidCommand(ContextState.Context, result.ErrorCode)
            );
        }

        await Mediator.Send(new SetProcessingOnWizardCommand(ContextState.Context, false));

        if (!Step.IsInvalid)
        {
            Data[$"Processor:{Step.Id}:Ran"] = "true";
            SuccessMessage = Localizer[
                "Successful Processed Request, you can Cancel/Close the Wizard."
            ];

            if (Step.Details["DisablePrevious"] == "true")
            {
                Data[$"DisablePrevious:{Step.Id}"] = "true";
            }

            foreach (var property in result.Result)
            {
                Data[property.Key] = property.Value;
            }

            await Mediator.Send(new UpdateWizardDataCommand(ContextState.Context, Data));

            if (Step.HasNext)
            {
                SuccessMessage = Localizer[
                    "Successful Processed Request, please continue to next step."
                ];

                if (
                    Step.Details.TryGetValue("Processor:AutoNext", out var autoNext)
                    && autoNext.Equals("true", StringComparison.InvariantCultureIgnoreCase)
                )
                {
                    await State.Next(ContextState.Context);
                }
            }
        }
    }
}
