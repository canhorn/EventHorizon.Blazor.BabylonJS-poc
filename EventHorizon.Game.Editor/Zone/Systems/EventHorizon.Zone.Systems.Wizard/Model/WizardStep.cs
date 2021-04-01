namespace EventHorizon.Zone.Systems.Wizard.Model
{
    public class WizardStep
    {
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public WizardStepDetails Details { get; set; } = new WizardStepDetails();
        public string? NextStep { get; set; }
        public string? PreviousStep { get; set; }

        public bool HasNext => !string.IsNullOrWhiteSpace(NextStep);
        public bool HasPrevious => !string.IsNullOrWhiteSpace(PreviousStep);

        public bool IsInvalid { get; set; }
        public string ErrorCode { get; set; } = string.Empty;
        public bool IsProcessing { get; set; }
        public void Reset()
        {
            IsInvalid = false;
            ErrorCode = string.Empty;
            IsProcessing = false;
        }
    }
}
