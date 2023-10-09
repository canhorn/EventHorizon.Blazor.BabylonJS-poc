namespace EventHorizon.Game.Client.Systems.Entity.Properties.Selection.Model;

using EventHorizon.Game.Client.Systems.Entity.Properties.Selection.Api;

public class SelectionStateModel : SelectionState
{
    public string SelectedCompanionParticleTemplate { get; set; } =
        string.Empty;
    public string SelectedParticleTemplate { get; set; } = string.Empty;
}
