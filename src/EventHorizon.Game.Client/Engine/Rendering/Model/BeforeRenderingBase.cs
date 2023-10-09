namespace EventHorizon.Game.Client.Engine.Rendering.Model;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Rendering.Api;

public class BeforeRenderingBase : IBeforeRendering
{
    private readonly IRenderingScene _renderingScene;

    public BeforeRenderingBase(IRenderingScene renderingScene)
    {
        _renderingScene = renderingScene;
    }

    public string Register(Func<Task> action)
    {
        return _renderingScene.RegisterAfterRender(action);
    }

    public Task UnRegister(string handle)
    {
        throw new NotImplementedException();
    }
}
