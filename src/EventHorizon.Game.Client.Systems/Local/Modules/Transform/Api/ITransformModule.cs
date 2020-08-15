namespace EventHorizon.Game.Client.Systems.Local.Modules.Transform.Api
{
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public interface ITransformModule
        : IModule
    {
        public static string MODULE_NAME = "TRANSFORM_MODULE_NAME";
        void Reset(
            IVector3 position
        );
        void SetRotation(
            IVector3 rotation
        );
    }
}
