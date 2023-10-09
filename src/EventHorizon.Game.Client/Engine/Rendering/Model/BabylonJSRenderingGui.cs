namespace EventHorizon.Game.Client.Engine.Rendering.Model;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Canvas;
using EventHorizon.Game.Client.Engine.Rendering.Api;

public class BabylonJSRenderingGui : IRenderingGui
{
    private IGuiCanvas? _guiCanvas;

    public int Priority => 0;

    public IGuiCanvas GetGuiCanvas()
    {
        return GetGuiCanvas<IGuiCanvas>();
    }

    public T GetGuiCanvas<T>()
        where T : class, IGuiCanvas
    {
        if (_guiCanvas is T typedEngine)
        {
            return typedEngine;
        }
        throw new GameRuntimeException(
            "gui_canvas_is_null",
            "The Gui Canvas has not been set, is currently null."
        );
    }

    public Task Initialize()
    {
        _guiCanvas = new BabylonJSGuiCanvas();
        return _guiCanvas.Initialize();
    }

    public async Task Dispose()
    {
        if (_guiCanvas != null)
        {
            await _guiCanvas.Dispose();
        }
    }
}
