namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Api
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public interface IInputModule
        : IModule
    {
        public static string MODULE_NAME = "INPUT_MODULE_NAME";

        // void registerInput(InputOptions options);
        // void unRegisterInput(InputOptions options);
        // void resetToDefaultLayout();
    }
}
