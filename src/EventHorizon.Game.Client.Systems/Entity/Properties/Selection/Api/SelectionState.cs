namespace EventHorizon.Game.Client.Systems.Entity.Properties.Selection.Api;

public interface SelectionState
{
    public static readonly string NAME = "selectionState";

    string SelectedCompanionParticleTemplate { get; }
    string SelectedParticleTemplate { get; }
}
