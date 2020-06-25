namespace EventHorizon.Game.Client.Engine.Rendering.Api
{
    using EventHorizon.Game.Client.Engine.Rendering.Model;

    public static class IRenderingSceneExtensions
    {
        public static BabylonJSSceneImplementation GetBabylonJSScene(
            this IRenderingScene renderingScene
        )
        {
            return renderingScene.GetScene<BabylonJSSceneImplementation>();
        }
    }
}
