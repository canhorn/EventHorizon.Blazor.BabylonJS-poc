namespace EventHorizon.Game.Client.Engine.Rendering.Model;

using System;
using System.Threading.Tasks;
using BabylonJS;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using EventHorizon.Game.Client.Engine.Window.Resize;

public class MyEngineOptions : EngineOptionsCachedEntity
{
    public bool preserveDrawingBuffer { get; set; }
}

public class BabylonJSEngineImplementation : IEngineImplementation, SystemWindowResizedEventObserver
{
    public BabylonJS.Engine Engine { get; }

    public BabylonJSEngineImplementation(
        Html.Interop.Canvas canvas,
        bool antialias,
        bool preserveDrawingBuffer
    )
    {
        Engine = new BabylonJS.Engine(
            canvas,
            antialias,
            new MyEngineOptions { preserveDrawingBuffer = preserveDrawingBuffer, }
        );
        GamePlatform.RegisterObserver(this);
    }

    public void DisplayLoadingUI()
    {
        Engine.displayLoadingUI();
    }

    public void Dispose()
    {
        Engine.dispose();
        GamePlatform.UnRegisterObserver(this);
    }

    public long GetDeltaTime()
    {
        return (long)Engine.getDeltaTime();
    }

    public void HideLoadingUI()
    {
        Engine.hideLoadingUI();
    }

    public string RunRenderLoop(Func<Task> action)
    {
        return Engine.runRenderLoop(action);
    }

    public Task Handle(SystemWindowResizedEvent args)
    {
        Engine.resize();

        return Task.CompletedTask;
    }
}
