namespace EventHorizon.Game.Client.Systems.Entity.Modules.Details.Api
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public interface DetailsModule
        : IModule
    {
        public static string MODULE_NAME => "DETAILS_MODULE_NAME";
    }
}
