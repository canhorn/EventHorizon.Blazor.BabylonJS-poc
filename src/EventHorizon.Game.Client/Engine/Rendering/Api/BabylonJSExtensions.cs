namespace EventHorizon.Game.Client.Engine.Rendering.Api
{
    using EventHorizon.Game.Client.Engine.Rendering.Model;

    public static class BabylonJSExtensions
    {
        public static BabylonJSSceneImplementation GetBabylonJSScene(
            this IRenderingScene renderingScene
        )
        {
            return renderingScene.GetScene<BabylonJSSceneImplementation>();
        }
        public static BabylonJSEngineImplementation GetBabylonJSEngine(
            this IRenderingEngine renderingEngine
        )
        {
            return renderingEngine.GetEngine<BabylonJSEngineImplementation>();
        }
    }
}
