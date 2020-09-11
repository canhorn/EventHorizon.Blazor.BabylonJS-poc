namespace EventHorizon.Game.Client.Systems.Local.Modules.State.Api
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public interface IStateModule
        : IModule
    {
        public static string MODULE_NAME => "STATE_MODULE_NAME";

        int Size { get; }

        void Add(
            IState state
        );
        void AddPriority(
            IState state
        );
        void Clear();
    }
}
