namespace EventHorizon.Game.Client.Engine.Rendering.Model
{
    using EventHorizon.Game.Client.Engine.Rendering.Api;

    public class BabylonJSEngineImplementation
        : BabylonJS.Engine, IEngineImplementation
    {
        public BabylonJSEngineImplementation(
            BabylonJS.Html.Canvas canvas,
            bool antialias,
            bool preserveDrawingBuffer
        ) : base(
            canvas,
            antialias,
            preserveDrawingBuffer
        )
        {
        }
    }
}