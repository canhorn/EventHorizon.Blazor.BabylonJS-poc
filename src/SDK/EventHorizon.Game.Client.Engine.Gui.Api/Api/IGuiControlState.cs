namespace EventHorizon.Game.Client.Engine.Gui.Api;

using System;

public interface IGuiControlState
{
    string GenerateId(string guiId, string controlId);
    void Set(string guiId, IGuiControl control);
    Option<IGuiControl> Get(string guiId, string controlId);
    void Remove(string guiId, string controlId);
    Option<IGuiControl> Get(string id);
}
