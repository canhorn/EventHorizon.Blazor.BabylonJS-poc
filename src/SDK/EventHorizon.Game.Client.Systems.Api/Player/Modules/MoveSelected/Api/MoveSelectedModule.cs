namespace EventHorizon.Game.Client.Systems.Player.Modules.MoveSelected.Api;

using System;
using EventHorizon.Game.Client.Engine.Systems.Module.Api;

public interface MoveSelectedModule : IModule
{
    public static string MODULE_NAME { get; } = "MOVE_SELECTED_MODULE_NAME";
}
