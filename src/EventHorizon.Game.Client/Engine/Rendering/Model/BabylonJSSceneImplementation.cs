namespace EventHorizon.Game.Client.Engine.Rendering.Model;

using System;
using System.Threading.Tasks;

using BabylonJS;

using EventHorizon.Game.Client.Engine.Rendering.Api;

public class BabylonJSSceneImplementation : ISceneImplementation
{
    public Scene Scene { get; private set; }

    public BabylonJSSceneImplementation(Engine engine)
    {
        Scene = new Scene(engine);
    }

    public void Dispose()
    {
        Scene.dispose();
    }

    public string RegisterBeforeRender(Func<Task> beforeRenderAction) =>
        Scene.registerBeforeRender(beforeRenderAction);

    public string RegisterAfterRender(Func<Task> afterRenderAction) =>
        Scene.registerAfterRender(afterRenderAction);

    public void Render() => Scene.render(true, false);
}
