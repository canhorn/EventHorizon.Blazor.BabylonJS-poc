namespace EventHorizon.Game.Client.Engine.Rendering.Model;

using EventHorizon.Game.Client.Engine.Rendering.Api;

public class RenderingTimeBase : IRenderingTime
{
    private readonly IRenderingEngine _renderingEngine;

    public long DeltaTime
    {
        get { return _renderingEngine.GetEngine().GetDeltaTime(); }
    }

    public RenderingTimeBase(IRenderingEngine renderingEngine)
    {
        _renderingEngine = renderingEngine;
    }
}
