namespace EventHorizon.Zone.Systems.Wizard.Run
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Zone.Systems.Wizard.Model;
    using MediatR;

    public struct RunWizardScriptProcessorCommand
        : IRequest<CommandResult<WizardData>>
    {
        public string WizardId { get; }
        public string WizardStepId { get; }
        public string ProcessorScriptId { get; }
        public WizardData WizardData { get; }

        public RunWizardScriptProcessorCommand(
            string wizardId,
            string wizardStepId,
            string processorScriptId,
            WizardData wizardData
        )
        {
            WizardId = wizardId;
            WizardStepId = wizardStepId;
            ProcessorScriptId = processorScriptId;
            WizardData = wizardData;
        }
    }
}
