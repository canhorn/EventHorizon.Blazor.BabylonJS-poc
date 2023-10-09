namespace EventHorizon.Game.Client.Systems.Player.Modules.SkillSelection.Api;

using EventHorizon.Game.Client.Engine.Systems.Module.Api;

public interface SkillSelectionModule : IModule
{
    public static string MODULE_NAME { get; } = "SKILL_SELECTION_MODULE_NAME";
}
