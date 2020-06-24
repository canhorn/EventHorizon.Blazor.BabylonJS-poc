namespace EventHorizon.Game.Client.Engine.Rendering.Api
{
    using System;
    using System.Threading.Tasks;

    public interface IEngineImplementation
    {
        void Dispose();
        string RunRenderLoop(Func<Task> action);
        void HideLoadingUI();
        void DisplayLoadingUI();
        long GetDeltaTime();
    }
}
