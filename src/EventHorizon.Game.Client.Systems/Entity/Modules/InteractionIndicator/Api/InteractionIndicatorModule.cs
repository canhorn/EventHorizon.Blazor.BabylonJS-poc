namespace EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Api
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public interface InteractionIndicatorModule
        : IModule
    {
        public static string MODULE_NAME => "INTERACTION_INDICATOR_MODULE_NAME";
    }
}
