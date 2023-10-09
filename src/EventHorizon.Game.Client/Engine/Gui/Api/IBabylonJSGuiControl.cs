namespace EventHorizon.Game.Client.Engine.Gui.Api;

using System;

using BabylonJS.GUI;

public interface IBabylonJSGuiControl : IGuiControl
{
    Control Control { get; }
}
