namespace EventHorizon.Game.Client.Engine.Gui.Canvas;

using System;
using System.Threading.Tasks;

using BabylonJS.GUI;

using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Rendering.Api;

public class BabylonJSGuiCanvas : IGuiCanvas
{
    private AdvancedDynamicTexture? _uiTexture;

    public Task Initialize()
    {
        _uiTexture = AdvancedDynamicTexture.CreateFullscreenUI(
            "ROOT_GUI",
            true,
            GameServiceProvider
                .GetService<IRenderingScene>()
                .GetBabylonJSScene()
                .Scene
        );
        return Task.CompletedTask;
    }

    public Task Dispose()
    {
        _uiTexture?.dispose();
        return Task.CompletedTask;
    }

    public void AddControl(IGuiControl control)
    {
        if (_uiTexture == null)
        {
            throw new GameException(
                "gui_canvas_not_initialized",
                "Gui Canvas was not initialized."
            );
        }
        if (control is IBabylonJSGuiControl castedControl)
        {
            _uiTexture.addControl(castedControl.Control);
        }
    }
}
