namespace EventHorizon.Game.Client.Engine.Gui.Api;

using System;
using System.Collections.Generic;

public interface IGuiControlChildrenState
{
    void AddChildGuiToControl(string controlId, string childGuiId);

    IEnumerable<string> GetChildren(string controlId);

    IEnumerable<string> RemoveTrackingOfControl(string controlId);
}
