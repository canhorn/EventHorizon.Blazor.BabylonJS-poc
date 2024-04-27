namespace EventHorizon.Game.Editor.Client.Wizard.Api;

public record WizardStateChangeArgs(string Reason, string? Context = null);
