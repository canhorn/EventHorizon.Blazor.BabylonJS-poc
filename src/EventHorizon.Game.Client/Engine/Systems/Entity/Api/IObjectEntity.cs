namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public interface IObjectEntity
        : IClientEntity
    {
        long EntityId { get; }
        string Name { get; }
        string GlobalId { get; }
        string Type { get; }
        ITransform Transform { get; }
        IList<string> Tags { get; }
        IObjectEntityDetails Details { get; }

        void RegisterModule(
            string name,
            IModule module
        );
        T GetModule<T>(
            string name
        ) where T : IModule;

        void SetProperty(
            string name,
            object property
        );
        T GetProperty<T>(
            string name
        );

        void UpdateDetails(
            IObjectEntityDetails details
        );
    }
}
