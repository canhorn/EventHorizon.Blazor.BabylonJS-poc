namespace EventHorizon.Game.Client.Systems.Entity.Modules.Move.Api
{
    using System;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public interface IMoveModule
        : IModule
    {
        public static string MODULE_NAME => "MOVE_MODULE_NAME";

        void SetCurrentMoveTo(
            IVector3 position
        );
    }
}
