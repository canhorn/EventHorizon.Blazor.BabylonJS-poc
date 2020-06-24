namespace EventHorizon.Game.Client.Engine.Rendering.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Rendering.Api;

    public class StandardEngineImplementation
        : IEngineImplementation
    {
        public static async Task<StandardEngineImplementation> Create(
            BabylonJS.Html.Canvas canvas,
            bool antialias,
            bool preserveDrawingBuffer
        )
        {
            return new StandardEngineImplementation(
                await BabylonJS.Engine.Create(
                   canvas,
                   antialias,
                   preserveDrawingBuffer
               )
            );
        }

        public BabylonJS.Engine Engine { get; }

        private StandardEngineImplementation(
            BabylonJS.Engine engine
        )
        {
            Engine = engine;
        }

        public void Dispose()
        {
            Engine.Dispose();
        }

        public void HideLoadingUI()
        {
            Engine.HideLoadingUI();
        }

        public void DisplayLoadingUI()
        {
            Engine.DisplayLoadingUI();
        }

        public long GetDeltaTime()
        {
            return Engine.GetDeltaTime();
        }

        public string RunRenderLoop(Func<Task> action)
        {
            return Engine.RunRenderLoop(action);
        }
    }
}